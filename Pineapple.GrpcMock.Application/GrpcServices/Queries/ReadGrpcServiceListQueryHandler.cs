using Mediator;
using Pineapple.GrpcMock.Application.GrpcServices.Registry;

namespace Pineapple.GrpcMock.Application.GrpcServices.Queries;

internal sealed class ReadGrpcServiceListQueryHandler : IQueryHandler<ReadGrpcServiceListQuery, ReadGrpcServiceListQueryResult>
{
    private readonly IGrpcServiceRegistry _registry;

    public ReadGrpcServiceListQueryHandler(IGrpcServiceRegistry registry)
    {
        _registry = registry;
    }

    public ValueTask<ReadGrpcServiceListQueryResult> Handle(ReadGrpcServiceListQuery query, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new ReadGrpcServiceListQueryResult(_registry.List()));
    }
}
