using System.Collections.Immutable;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Infrastructure.Registry.Stubs;

public class StubRegistry : IStubRegistry
{
    private static readonly Dictionary<StubRegistryKeyDto, IList<StubRegistryValueDto>> _registry = new();

    public void Add(StubRegistryKeyDto key, StubRegistryValueDto value)
    {
        if (_registry.TryGetValue(key, out var values))
        {
            values.Add(value);
            return;
        }
        _registry.Add(key, new List<StubRegistryValueDto>() { value });
    }

    public IReadOnlyList<StubRegistryValueDto> Get(StubRegistryKeyDto key)
    {
        return _registry.GetValueOrDefault(key)?.ToArray() ?? Array.Empty<StubRegistryValueDto>();
    }

    public IReadOnlyDictionary<StubRegistryKeyDto, IReadOnlyList<StubRegistryValueDto>> List()
    {
        return _registry.ToDictionary(
            kvp => kvp.Key,
            kvp => (IReadOnlyList<StubRegistryValueDto>) kvp.Value.AsReadOnly()
        );
    }

    public void Remove(StubRegistryKeyDto key)
    {
        _registry.Remove(key);
    }
}