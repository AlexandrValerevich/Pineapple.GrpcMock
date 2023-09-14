using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry.Dto;

namespace Pineapple.GrpcMock.Application.Common.GrpcServices.Registry;

public interface IGrpcServicesRegistry
{
    IReadOnlyList<GrpcServiceMetaDto> List();
    GrpcServiceMetaDto? Get(string shortName);
}
