using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Pineapple.GrpcMock.RpcHost.Rpc.Interceptors;

internal sealed class ExceptionHandlingServerInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex) when (ex is not RpcException)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}