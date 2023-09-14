namespace Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

public record StubRegistryKeyDto
(
    string ServiceShortName,
    string Method
);