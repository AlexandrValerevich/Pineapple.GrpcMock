using System.Reflection;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
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
    private readonly ILoggingClientInterceptorFactory _loggerInterceptorFactory;

    public ProxyService(IProxyRegistry proxyRegistry, IProtoMetaRegistry protoMetaRegistry, ILoggingClientInterceptorFactory loggerInterceptorFactory)
    {
        _proxyRegistry = proxyRegistry;
        _protoMetaRegistry = protoMetaRegistry;
        _loggerInterceptorFactory = loggerInterceptorFactory;
    }

    public async Task<OneOf<ProxyGrpcRequestResultDto, RpcException, NotFound>> Proxy(ProxyGrpcRequestQueryDto query, CancellationToken cancellationToken = default)
    {
        var canProxy = _proxyRegistry.TryGet(new ProxyRegistryKeyDto(ServiceShortName: query.ServiceShortName), out ProxyRegistryUrlDto? url);
        if (!canProxy)
            return new NotFound();

        var serviceMeta = _protoMetaRegistry.Get(query.ServiceShortName);
        if (serviceMeta is null)
            return new NotFound();

        // Create an instance of the gRPC service client
        using var channel = GrpcChannel.ForAddress(url!.Value);
        var callInvoker = channel.Intercept(_loggerInterceptorFactory.Create());
        try
        {
            object? serviceClient = Activator.CreateInstance(serviceMeta.ClientType, callInvoker);

            // Find the gRPC method
            MethodInfo? grpcMethod = FindGrpcMethod(serviceMeta.ClientType, query.MethodName + "Async", query.Request);
            if (grpcMethod is null)
                return new NotFound();

            // Invoke the gRPC method dynamically
            var asyncUnaryCall = grpcMethod.Invoke(serviceClient, new object[] { query.Request, default(Metadata?)!, default(DateTime?)!, default(CancellationToken) })!;

            // Get the ResponseAsync property using reflection
            var responseAsyncProperty = asyncUnaryCall.GetType().GetProperty("ResponseAsync");
            var responseTask = (Task) responseAsyncProperty?.GetValue(asyncUnaryCall)!;

            // Await the response task and access metadata and status
            await responseTask.ConfigureAwait(false);

            // Get the Result property from the completed task
            var resultProperty = responseTask.GetType().GetProperty("Result");
            var response = resultProperty?.GetValue(responseTask);

            // Get the status and metadata
            var getStatusMethod = asyncUnaryCall.GetType().GetMethod("GetStatus");
            var getTrailersMethod = asyncUnaryCall.GetType().GetMethod("GetTrailers");

            var status = (Status?) getStatusMethod?.Invoke(asyncUnaryCall, null);
            var metadata = (Metadata?) getTrailersMethod?.Invoke(asyncUnaryCall, null);

            return new ProxyGrpcRequestResultDto(
                Response: (IMessage) response!,
                Status: status ?? Status.DefaultSuccess,
                Metadata: metadata ?? new Metadata()
            );
        }
        catch (TargetInvocationException ex) when (ex.InnerException is RpcException exception)
        {
            return exception;
        }
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
