using Pineapple.GrpcMock.Application.Common.Stub.Registry.Dto;

namespace Pineapple.GrpcMock.Application.Common.Stub.Registry;

public interface IStubRegistry
{
    Dictionary<StubRegistryKeyDto, StubRegistryValueDto> Registry { get; }
}
