using Pineapple.GrpcMock.Application.GrpcServices.Dto;

namespace Pineapple.GrpcMock.Application.GrpcServices.Registry;

public interface IGrpcServiceRegistry
{
    IReadOnlyList<GrpcServiceMetaDto> List();
    GrpcServiceMetaDto? Get(string shortName);
}
