namespace Pineapple.GrpcMock.Application.ProtoMeta.Dto;

public record ProtoServiceMetaDto(
    string ShortName,
    Type ServiceType,
    Type ClientType,
    IReadOnlyList<ProtoMethodMetaDto> Methods);