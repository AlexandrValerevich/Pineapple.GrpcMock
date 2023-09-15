namespace Pineapple.GrpcMock.Application.GrpcServices.Dto;

public record GrpcServiceMetaDto(
    string ShortName,
    IReadOnlyList<GrpcServiceMethodMetaDto> Methods
);