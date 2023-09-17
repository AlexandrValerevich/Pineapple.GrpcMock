namespace Pineapple.GrpcMock.Application.ProtoMeta.Dto;

public record ProtoServiceMetaDto(
    string ShortName,
    Type ServiceType,
    IReadOnlyList<ProtoMethodMetaDto> Methods);