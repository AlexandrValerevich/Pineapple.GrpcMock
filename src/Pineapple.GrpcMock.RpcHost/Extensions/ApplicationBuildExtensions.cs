using System.Collections.Immutable;
using System.Text.Json;
using Mediator;
using Microsoft.Extensions.Options;
using Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Pineapple.GrpcMock.Contracts.Stubs.V1;
using Pineapple.GrpcMock.RpcHost.Configurations;
using Throw;

namespace Pineapple.GrpcMock.RpcHost.Extensions;

internal static class ApplicationBuildExtensions
{
    public static IApplicationBuilder InitializeStubs(this IApplicationBuilder app)
    {
        StubOptions stubOptions = app.ApplicationServices.GetRequiredService<IOptions<StubOptions>>().Value;
        IEnumerable<AddStubApiRequest> stubs = ReadStubs(stubOptions.Folder);
        var mediator = app.ApplicationServices.GetRequiredService<IMediator>();

        foreach (AddStubApiRequest stub in stubs)
        {
            var result = mediator.Send(new AddStubCommand(
                ServiceShortName: stub.ServiceShortName,
                Method: stub.Method,
                RequestBody: stub.Request.Body,
                ResponseBody: stub.Response.Body,
                Status: new StubStatusDto(
                    Code: stub.Response.Status.Code,
                    Details: stub.Response.Status.Details),
                Metadata: new StubMetadataDto(stub.Response.Metadata.Trailer.ToImmutableDictionary()),
                Delay: stub.Response.Delay,
                Priority: stub.Priority));

            result.AsTask().Wait();
        }

        return app;
    }

    private static IEnumerable<AddStubApiRequest> ReadStubs(string path)
    {
        var stubs = new List<AddStubApiRequest>();
        string[] jsonFiles = Directory.GetFiles(path, "*.json");
        foreach (string filePath in jsonFiles)
        {
            ReadOnlySpan<byte> jsonContent = File.ReadAllBytes(filePath).AsSpan();
            stubs.Add(JsonSerializer.Deserialize<AddStubApiRequest>(jsonContent).ThrowIfNull());
        }

        return stubs;
    }
}