using System.Collections.Immutable;
using System.Text.Json;
using ErrorOr;
using Mediator;
using Microsoft.Extensions.Options;
using Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Pineapple.GrpcMock.Contracts.Stubs.V1;
using Pineapple.GrpcMock.RpcHost.Configurations;
using Throw;

namespace Pineapple.GrpcMock.RpcHost.Workers;

internal sealed class StubInitializationWorker : IHostedService
{
    private readonly IMediator _mediator;
    private readonly StubOptions _stubOptions;
    private readonly ILogger _logger;

    public StubInitializationWorker(IOptions<StubOptions> stubOptions,
        IMediator mediator,
        ILogger<StubInitializationWorker> logger)
    {
        _mediator = mediator;
        _stubOptions = stubOptions.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        IEnumerable<AddStubApiRequest> stubs = ReadStubs(_stubOptions.Folder);
        foreach (AddStubApiRequest stub in stubs)
        {
            try
            {
                ErrorOr<Unit> result = await _mediator.Send(new AddStubCommand(
                    ServiceShortName: stub.ServiceShortName,
                    Method: stub.Method,
                    RequestBody: stub.Request.Body,
                    ResponseBody: stub.Response.Body,
                    Status: new StubStatusDto(
                        Code: stub.Response.Status.Code,
                        Details: stub.Response.Status.Details),
                    Metadata: new StubMetadataDto(stub.Response.Metadata.Trailer.ToImmutableDictionary()),
                    Delay: stub.Response.Delay,
                    Priority: stub.Priority), cancellationToken);

                if (result.IsError)
                {
                    _logger.LogError("Can't add stub for [{ServiceName}/{Method}] during initialization. Errors: {Errors}", stub.ServiceShortName, stub.Method, result.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't add stub for [{ServiceName}/{Method}] during initialization", stub.ServiceShortName, stub.Method);
            }
        }

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private IEnumerable<AddStubApiRequest> ReadStubs(string path)
    {
        var stubs = new List<AddStubApiRequest>();
        string[] stubFiles = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
        foreach (string stubFile in stubFiles)
        {
            _logger.LogTrace("Read stub from file: {StubFile}", stubFile);
            ReadOnlySpan<byte> jsonContent = File.ReadAllBytes(stubFile).AsSpan();
            stubs.Add(JsonSerializer.Deserialize<AddStubApiRequest>(jsonContent).ThrowIfNull());
        }

        return stubs;
    }
}
