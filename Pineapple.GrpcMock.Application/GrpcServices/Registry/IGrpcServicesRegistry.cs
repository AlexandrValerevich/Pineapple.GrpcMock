using Pineapple.GrpcMock.Application.GrpcServices.Registry.Dto;

namespace Pineapple.GrpcMock.Application.GrpcServices.Registry;

public interface IGrpcServicesRegistry
{
    IReadOnlyList<GrpcServiceMetaDto> List();
    GrpcServiceMetaDto? Get(string shortName);
}
