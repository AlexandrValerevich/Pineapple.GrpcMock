using Pineapple.GrpcMock.Application.Common.Stub.Registry;
using Pineapple.GrpcMock.Application.Common.Stub.Registry.Dto;

namespace Pineapple.GrpcMock.Infrastructure.Stubs.Registry;

public class StubRegistry : IStubRegistry
{
    public Dictionary<StubRegistryKeyDto, StubRegistryValueDto> Registry => new();
}
