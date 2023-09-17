using Google.Protobuf;
using Grpc.Core;

namespace Pineapple.GrpcMock.Application.Stubs.Dto;

public record StubRegistryValueDto(
    IMessage Response,
    IMessage Request,
    Status Status,
    Metadata Metadata);