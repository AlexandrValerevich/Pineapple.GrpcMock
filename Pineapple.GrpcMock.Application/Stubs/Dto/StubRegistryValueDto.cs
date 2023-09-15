using Google.Protobuf;

namespace Pineapple.GrpcMock.Application.Stubs.Dto;

public record StubRegistryValueDto(
    IMessage Response,
    IMessage Request);