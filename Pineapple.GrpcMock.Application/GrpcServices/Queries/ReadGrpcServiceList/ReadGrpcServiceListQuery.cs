using Mediator;

namespace Pineapple.GrpcMock.Application.GrpcServices.Queries.ReadGrpcServiceList;

public record ReadGrpcServiceListQuery : IQuery<ReadGrpcServiceListQueryResult>
{
    public static readonly ReadGrpcServiceListQuery Instance = new();
};