using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies.Dto;

namespace Pineapple.GrpcMock.Application.Proxies;

internal sealed class ProxyService : IProxyService
{
    private readonly IProxyRegistry _registry;

    public ProxyService(IProxyRegistry registry)
    {
        _registry = registry;
    }

    public bool Can(CanProxyQueryDto query)
    {
        return _registry.Contain(new ProxyRegistryKeyDto(ParseServiceShortName(query.RequestUrlPath)));
    }

    public string GetUrl(GetProxyUrlQueryDto query)
    {
        if (!_registry.TryGet(new ProxyRegistryKeyDto(ParseServiceShortName(query.RequestUrlPath)), out var url))
            throw new Exception("Try get not existed");

        return url!.Value;
    }

    private static string ParseServiceShortName(string requestUrlPath)
    {
        return requestUrlPath[1..]
            .Split('/').First()
            .Split('.').Last();
    }
}
