using Pineapple.GrpcMock.Application.Stubs.Registry;
using Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

namespace Pineapple.GrpcMock.Infrastructure.Stubs.Registry;

public class StubRegistry : IStubRegistry
{
    public Dictionary<StubRegistryKeyDto, StubRegistryValueDto> Registry => new();
}
