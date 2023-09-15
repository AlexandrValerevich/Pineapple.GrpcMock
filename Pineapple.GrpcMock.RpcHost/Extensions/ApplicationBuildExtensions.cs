using System.Text.Json;
using Mediator;
using Microsoft.Extensions.Options;
using Pineapple.GrpcMock.Application.Stubs.Commands;
using Pineapple.GrpcMock.Contracts.Stubs.V1;
using Pineapple.GrpcMock.RpcHost.Configurations;
using Throw;

namespace Pineapple.GrpcMock.RpcHost.Extensions;

internal static class ApplicationBuildExtensions
{
    public static IApplicationBuilder InitializeStubs(this IApplicationBuilder app)
    {
        StubOptions stubOptions = app.ApplicationServices.GetRequiredService<IOptions<StubOptions>>().Value;
        IEnumerable<StubApiRequest> stubs = ReadStubs(stubOptions.Folder);
        var mediator = app.ApplicationServices.GetRequiredService<IMediator>();

        foreach (StubApiRequest stub in stubs)
        {
            var result = mediator.Send(new AddStubCommand(
                ServiceShortName: stub.ServiceShortName,
                ServiceMethod: stub.ServiceMethod,
                RequestBody: stub.Request.Body.ToString(),
                ResponseBody: stub.Response.Body.ToString()
            ));

            result.AsTask().Wait();
        }

        return app;
    }

    private static IEnumerable<StubApiRequest> ReadStubs(string path)
    {
        var stubs = new List<StubApiRequest>();
        string[] jsonFiles = Directory.GetFiles(path, "*.json");
        foreach (string filePath in jsonFiles)
        {
            ReadOnlySpan<byte> jsonContent = File.ReadAllBytes(filePath).AsSpan();
            stubs.Add(JsonSerializer.Deserialize<StubApiRequest>(jsonContent).ThrowIfNull());
        }

        return stubs;
    }
}