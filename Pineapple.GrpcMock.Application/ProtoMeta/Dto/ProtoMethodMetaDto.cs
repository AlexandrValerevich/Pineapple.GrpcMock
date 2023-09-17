namespace Pineapple.GrpcMock.Application.ProtoMeta.Dto;

public record ProtoMethodMetaDto(
    string Name,
    Type InputType,
    Type OutputType);