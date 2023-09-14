using Google.Protobuf;

namespace Pineapple.GrpcMock.Application.Stubs.Registry.Dto;

public record StubRegistryValueDto
(
    IMessage Response,
    IMessage Request
);
