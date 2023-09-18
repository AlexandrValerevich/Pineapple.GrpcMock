namespace Pineapple.GrpcMock.Application.Stubs.Dto;

public record StubRegistryKeyDto(
    string ServiceShortName,
    string Method);