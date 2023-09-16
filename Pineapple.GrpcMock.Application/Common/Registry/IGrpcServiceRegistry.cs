using Pineapple.GrpcMock.Application.GrpcServices.Dto;

namespace Pineapple.GrpcMock.Application.Common.Registry;

public interface IGrpcServiceRegistry
{
    IReadOnlyList<GrpcServiceMetaDto> List();
    GrpcServiceMetaDto? Get(string shortName);
}