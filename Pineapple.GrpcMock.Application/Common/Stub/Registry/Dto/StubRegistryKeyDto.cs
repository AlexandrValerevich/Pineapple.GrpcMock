namespace Pineapple.GrpcMock.Application.Common.Stub.Registry.Dto;

public record StubRegistryKeyDto
(
    string ShortServiceName,
    string ServiceMethod,
    byte[] RequestBody
);
