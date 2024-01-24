using System.Collections.Immutable;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies.Dto;

namespace Pineapple.GrpcMock.Infrastructure.Registry.Proxies;

internal sealed class ProxyRegistry : IProxyRegistry
{
    private static readonly Dictionary<ProxyRegistryKeyDto, ProxyRegistryUrlDto> _registry = new();

    public void AddOrUpdate(ProxyRegistryKeyDto key, ProxyRegistryUrlDto url)
    {
        if (_registry.ContainsKey(key))
        {
            _registry[key] = url;
        }
        else
        {
            _registry.Add(key, url);
        }
    }

    public bool Contain(ProxyRegistryKeyDto key)
    {
        return _registry.ContainsKey(key);
    }

    public void Delete(ProxyRegistryKeyDto key)
    {
        _registry.Remove(key);
    }

    public IReadOnlyList<(string ServiceShortName, string ProxyTo)> List()
    {
        return _registry.Select(x => (x.Key.ServiceShortName, x.Value.Value)).ToImmutableList();
    }

    public bool TryGet(ProxyRegistryKeyDto key, out ProxyRegistryUrlDto? value)
    {
        return _registry.TryGetValue(key,out value);
    }

}
