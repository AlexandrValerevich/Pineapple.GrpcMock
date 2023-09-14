using Pineapple.GrpcMock.Application.Common.Stub.Registry.Dto;

namespace Pineapple.GrpcMock.Application.Common.Stub.Registry;

public interface IStubRegistry
{
    void Add(StubRegistryKeyDto key, StubRegistryValueDto value);
    StubRegistryValueDto? Get(StubRegistryKeyDto key);
}
