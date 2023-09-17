using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList;

internal sealed class ReadProtoMetaListQueryHandler : IQueryHandler<ReadProtoMetaListQuery, ReadProtoMetaListQueryResult>
{
    private readonly IProtoMetaRegistry _registry;

    public ReadProtoMetaListQueryHandler(IProtoMetaRegistry registry)
    {
        _registry = registry;
    }

    public ValueTask<ReadProtoMetaListQueryResult> Handle(ReadProtoMetaListQuery query, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new ReadProtoMetaListQueryResult(_registry.List()));
    }
}