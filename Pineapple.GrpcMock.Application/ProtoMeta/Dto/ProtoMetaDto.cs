namespace Pineapple.GrpcMock.Application.ProtoMeta.Dto;

public record ProtoMetaDto(
    string ShortName,
    Type ServiceType,
    IReadOnlyList<ProtoMethodMetaDto> Methods);