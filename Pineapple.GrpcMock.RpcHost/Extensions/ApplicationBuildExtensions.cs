using System.Reflection;
using System.Text.Json;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Pineapple.GrpcMock.Application.Stubs.Registry;
using Pineapple.GrpcMock.Application.Stubs.Registry.Dto;
using Pineapple.GrpcMock.Contracts.Stubs.V1;
using Pineapple.GrpcMock.RpcHost.Configurations;
using Throw;

namespace Pineapple.GrpcMock.RpcHost.Extensions;

internal static class ApplicationBuildExtensions
{
    public static IApplicationBuilder SetUpStubs(this IApplicationBuilder app)
    {
        StubOptions stubOptions = app.ApplicationServices.GetRequiredService<IOptions<StubOptions>>().Value;
        IEnumerable<StubApiRequest> stubs = ReadStubs(stubOptions.Folder);

        var stubRegistry = app.ApplicationServices.GetRequiredService<IStubRegistry>();
        IEnumerable<Type> grpcServiceTypes = Assembly.GetExecutingAssembly().GetGrpcServices();

        foreach (StubApiRequest stub in stubs)
        {
            var key = new StubRegistryKeyDto(
                ServiceName: stub.ServiceName,
                ServiceMethod: stub.ServiceMethod,
                RequestBody: stub.Request.Body
            );

            Type service = grpcServiceTypes.Single(t => t.Name.ToLower() == $"{stub.ServiceName}Base".ToLower());
            MethodInfo serviceMethod = service.GetMethod(stub.ServiceMethod).ThrowIfNull();
            Type responseType = serviceMethod.ReturnType.GenericTypeArguments.First();

            var response = JsonSerializer.Deserialize(stub.Response.Body, responseType) as IMessage;
            var value = new StubRegistryValueDto(response.ToByteArray());

            stubRegistry.Registry.Add(key, value);
        }

        return app;
    }

    private static IEnumerable<StubApiRequest> ReadStubs(string path)
    {
        var stubs = new List<StubApiRequest>();
        string[] jsonFiles = Directory.GetFiles(path, "*.json");
        foreach (string filePath in jsonFiles)
        {
            string jsonContent = File.ReadAllText(filePath);
            stubs.Add(JsonSerializer.Deserialize<StubApiRequest>(jsonContent).ThrowIfNull());
        }

        return stubs;
    }
}
