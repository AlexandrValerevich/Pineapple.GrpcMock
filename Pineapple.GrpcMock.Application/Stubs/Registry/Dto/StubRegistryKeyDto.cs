namespace Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

public record StubRegistryKeyDto
(
    string ShortServiceName,
    string ServiceMethod,
    byte[] RequestBody
);
