using Pineapple.GrpcMock.Application.Stubs.Registry;
using Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

namespace Pineapple.GrpcMock.Infrastructure.Stubs.Registry;

public class StubRegistry : IStubRegistry
{
    private static readonly Dictionary<StubRegistryKeyDto, StubRegistryValueDto> _registry = new();

    public void Add(StubRegistryKeyDto key, StubRegistryValueDto value)
    {
        _registry.Add(key, value);
    }

    public StubRegistryValueDto? Get(StubRegistryKeyDto key)
    {
        return _registry.GetValueOrDefault(key);
    }
}
