namespace Pineapple.GrpcMock.Application.GrpcServices.Dto;

public record GrpcServiceMetaDto(
    string ShortName,
    Type ServiceType,
    IReadOnlyList<GrpcServiceMethodMetaDto> Methods);