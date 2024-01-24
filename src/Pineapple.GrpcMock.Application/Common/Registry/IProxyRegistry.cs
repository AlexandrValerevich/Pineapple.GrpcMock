using Pineapple.GrpcMock.Application.Proxies.Dto;

namespace Pineapple.GrpcMock.Application.Common.Registry;

public interface IProxyRegistry
{
    IReadOnlyList<(string ServiceShortName, string ProxyTo)> List();
    void AddOrUpdate(ProxyRegistryKeyDto key, ProxyRegistryUrlDto url);
    void Delete(ProxyRegistryKeyDto key);
    bool TryGet(ProxyRegistryKeyDto key, out ProxyRegistryUrlDto? url);
    bool Contain(ProxyRegistryKeyDto key);
}
