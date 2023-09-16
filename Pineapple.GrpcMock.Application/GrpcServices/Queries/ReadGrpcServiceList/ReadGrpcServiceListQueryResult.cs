using Pineapple.GrpcMock.Application.GrpcServices.Dto;

namespace Pineapple.GrpcMock.Application.GrpcServices.Queries.ReadGrpcServiceList;

public record ReadGrpcServiceListQueryResult(
   IReadOnlyList<GrpcServiceMetaDto> ServicesMeta);