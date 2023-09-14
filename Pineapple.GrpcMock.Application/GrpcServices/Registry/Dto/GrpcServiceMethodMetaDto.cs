namespace Pineapple.GrpcMock.Application.GrpcServices.Registry.Dto;

public class GrpcServiceMethodMetaDto
{
    public required string Name { get; set; }
    public required Type InputType { get; set; }
    public required Type OutputType { get; set; }
}
