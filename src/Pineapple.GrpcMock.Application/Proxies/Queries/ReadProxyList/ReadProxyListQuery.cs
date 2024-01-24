using System.Collections.Immutable;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Proxies.Dto;

namespace Pineapple.GrpcMock.Application.Proxies.Queries.ReadProxyList;

public sealed record ReadProxyListQuery : IQuery<ReadProxyListQueryResult>;

public sealed record ReadProxyListQueryResult(IReadOnlyList<ProxyItemDto> Proxies);

internal sealed class ReadProxyListQueryHandler : IQueryHandler<ReadProxyListQuery, ReadProxyListQueryResult>
{
    private readonly IProxyRegistry _registry;

    public ReadProxyListQueryHandler(IProxyRegistry registry)
    {
        _registry = registry;
    }

    public ValueTask<ReadProxyListQueryResult> Handle(ReadProxyListQuery query, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new ReadProxyListQueryResult(
            Proxies: _registry.List()
                .Select(x => new ProxyItemDto(x.ServiceShortName, x.ProxyTo))
                .ToImmutableList()
        ));
    }
}


