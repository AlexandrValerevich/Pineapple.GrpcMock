namespace Pineapple.GrpcMock.Application.GrpcServices.Dto;

public record GrpcServiceMethodMetaDto(
    string Name,
    Type InputType,
    Type OutputType);