using Grpc.Core;
using OneOf;
using OneOf.Types;
using Pineapple.GrpcMock.Application.Proxies.Services.Dto;

namespace Pineapple.GrpcMock.Application.Proxies;

public interface IProxyService
{
    /// <summary>
    /// Proxy grpc request if there url and proto for service
    /// </summary>
    /// <param name="query"> Proxy information </param>
    /// <returns> Response, status and meta </returns>
    OneOf<ProxyGrpcRequestResultDto, RpcException, NotFound> Proxy(ProxyGrpcRequestQueryDto query);
}
