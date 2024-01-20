using ErrorOr;
using Grpc.Core;

namespace Pineapple.GrpcMock.RpcHost.Rpc.Interceptors.Extensions;

internal static class ErrorExtensions
{
    public static StatusCode ToStatusCode(this Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => StatusCode.NotFound,
            ErrorType.Validation => StatusCode.InvalidArgument,
            ErrorType.Conflict => StatusCode.AlreadyExists,
            _ => StatusCode.Internal
        };
    }
}