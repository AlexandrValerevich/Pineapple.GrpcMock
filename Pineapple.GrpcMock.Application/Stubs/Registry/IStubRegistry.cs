using Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Registry;

public interface IStubRegistry
{
    Dictionary<StubRegistryKeyDto, StubRegistryValueDto> Registry { get; }
}
