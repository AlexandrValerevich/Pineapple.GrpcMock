using System.Reflection;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies.Dto;
using Pineapple.GrpcMock.Application.Proxies.Services.Dto;
using Pineapple.GrpcMock.Shared.Interceptors;

namespace Pineapple.GrpcMock.Application.Proxies;

internal sealed class ProxyService : IProxyService
{
    private readonly IProxyRegistry _proxyRegistry;
    private readonly IProtoMetaRegistry _protoMetaRegistry;
    private readonly ILoggerFactory _loggerFactory;

    public ProxyService(IProxyRegistry proxyRegistry, IProtoMetaRegistry protoMetaRegistry, ILoggerFactory loggerFactory)
    {
        _proxyRegistry = proxyRegistry;
        _protoMetaRegistry = protoMetaRegistry;
        _loggerFactory = loggerFactory;
    }

    public OneOf<ProxyGrpcRequestResultDto, RpcException, NotFound> Proxy(ProxyGrpcRequestQueryDto query)
    {
        var canProxy = _proxyRegistry.TryGet(new ProxyRegistryKeyDto(ServiceShortName: query.ServiceShortName), out ProxyRegistryUrlDto? url);
        if (!canProxy)
            return new NotFound();

        var serviceMeta = _protoMetaRegistry.Get(query.ServiceShortName);
        if (serviceMeta is null)
            return new NotFound();

        // Create an instance of the gRPC service client
        using var channel = CreateChannel(url!.Value);
        try
        {
            var serviceClient = Activator.CreateInstance(serviceMeta.ClientType, channel);

            // Find the gRPC method
            MethodInfo? grpcMethod = FindGrpcMethod(serviceMeta.ClientType, query.MethodName, query.Request);
            if (grpcMethod is null)
                return new NotFound();

            // Invoke the gRPC method dynamically
            var response = (IMessage) grpcMethod.Invoke(serviceClient, new object[] { query.Request, default(Metadata?)!, default(DateTime?)!, default(CancellationToken) })!;

            // Get the result property from the completed task
            // Extract gRPC status and metadata
            var status = Status.DefaultSuccess;
            var metadata = new Metadata();

            return new ProxyGrpcRequestResultDto(
                Response: response,
                Status: status,
                Metadata: metadata
            );
        }
        catch (TargetInvocationException ex) when (ex.InnerException is RpcException exception)
        {
            return exception;
        }
    }

    private GrpcChannel CreateChannel(string url)
    {
        var channel = GrpcChannel.ForAddress(url);
        channel.Intercept(new LoggingClientInterceptor(_loggerFactory.CreateLogger<LoggingClientInterceptor>()));
        return channel;
    }

    private static MethodInfo? FindGrpcMethod(Type clientType, string methodName, object request)
    {
        return clientType.GetMethods()
            .Where(m => m.Name == methodName)
            .FirstOrDefault(m =>
            {
                var parameters = m.GetParameters();

                if (parameters.Length != 4)
                    return false;

                // Check if the parameter type matches the request type or is assignable from it
                return parameters[0].ParameterType.IsAssignableFrom(request.GetType());
            });
    }

}
