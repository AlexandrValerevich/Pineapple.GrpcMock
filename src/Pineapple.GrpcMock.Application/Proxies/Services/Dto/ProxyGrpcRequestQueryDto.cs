using Google.Protobuf;
using Grpc.Core;

namespace Pineapple.GrpcMock.Application.Proxies.Services.Dto;

public record ProxyGrpcRequestQueryDto(
    string ServiceShortName,
    string MethodName,
    IMessage Request);

public record ProxyGrpcRequestResultDto(
    IMessage Response,
    Status Status,
    Metadata Metadata);
