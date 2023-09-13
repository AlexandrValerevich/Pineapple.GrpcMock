namespace Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

public record StubRegistryKeyDto
(
    string ServiceName,
    string ServiceMethod,
    string RequestBody
);
