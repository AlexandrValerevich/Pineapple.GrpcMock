using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Common.Registry;

public interface IStubRegistry
{
    void Add(StubRegistryKeyDto key, StubRegistryValueDto value);
    IReadOnlyList<StubRegistryValueDto> Get(StubRegistryKeyDto key);
    IReadOnlyDictionary<StubRegistryKeyDto, IReadOnlyList<StubRegistryValueDto>> List();
    void Remove(StubRegistryKeyDto key);
}