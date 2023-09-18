using Mediator;

namespace Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList;

public record ReadProtoMetaListQuery : IQuery<ReadProtoMetaListQueryResult>
{
    public static readonly ReadProtoMetaListQuery Instance = new();
};