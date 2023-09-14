namespace Pineapple.GrpcMock.Application.GrpcServices.Registry.Dto;

public class GrpcServiceMetaDto
{
    public required string ShortName { get; set; }
    public required IReadOnlyList<GrpcServiceMethodMetaDto> Methods { get; set; }
}
