using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList;

public record ReadStubListQuery : IQuery<ReadStubListQueryResult>
{
    public static readonly ReadStubListQuery Instance = new();
};
