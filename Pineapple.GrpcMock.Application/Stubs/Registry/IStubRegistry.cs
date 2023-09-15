using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Registry;

public interface IStubRegistry
{
    void Add(StubRegistryKeyDto key, StubRegistryValueDto value);
    IReadOnlyList<StubRegistryValueDto> Get(StubRegistryKeyDto key);
}