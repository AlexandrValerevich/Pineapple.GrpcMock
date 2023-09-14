using Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Registry;

public interface IStubRegistry
{
    void Add(StubRegistryKeyDto key, StubRegistryValueDto value);
    StubRegistryValueDto? Get(StubRegistryKeyDto key);
}
