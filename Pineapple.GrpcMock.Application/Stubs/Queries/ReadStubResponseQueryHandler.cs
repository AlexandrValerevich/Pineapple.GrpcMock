using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.Application.Stubs.Registry;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

public class ReadStubResponseQueryHandler : IQueryHandler<ReadStubResponseQuery, ReadStubResponseQueryResult>
{
    private readonly IStubRegistry _stubs;
    private readonly ILogger _logger;

    public ReadStubResponseQueryHandler(IStubRegistry stubs, ILogger<ReadStubResponseQueryHandler> logger)
    {
        _logger = logger;
        _stubs = stubs;
    }

    public ValueTask<ReadStubResponseQueryResult> Handle(ReadStubResponseQuery query, CancellationToken cancellationToken)
    {
        var shortName = query.ServiceFullName.Split(".").Last();

        _logger.LogTrace("{@Query}", query);

        var values = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method));

        return ValueTask.FromResult(new ReadStubResponseQueryResult(Response: values[0].Response));
    }
}
