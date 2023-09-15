using Pineapple.GrpcMock.Application.GrpcServices.Dto;

namespace Pineapple.GrpcMock.Application.GrpcServices.Queries;

public record ReadGrpcServiceListQueryResult(
   IReadOnlyList<GrpcServiceMetaDto> ServicesMeta
);

