using ErrorOr;
using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse;

public class ReadStubResponseQueryHandler : IQueryHandler<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>>
{
    private readonly IStubRegistry _stubs;
    private readonly ILogger _logger;

    public ReadStubResponseQueryHandler(IStubRegistry stubs, ILogger<ReadStubResponseQueryHandler> logger)
    {
        _stubs = stubs;
        _logger = logger;
    }

    public async ValueTask<ErrorOr<ReadStubResponseQueryResult>> Handle(ReadStubResponseQuery query, CancellationToken cancellationToken)
    {
        var shortName = query.ServiceFullName.Split(".").Last();

        var values = _stubs.Get(new(
            ServiceShortName: shortName,
            Method: query.Method));

        StubRegistryValueDto? value = values.Where(x => x.Request.Equals(query.Request)).MaxBy(x => x.Priority);
        if (value is null)
            return Errors.Stubs.StubNotFound;

        if (value.Delay != TimeSpan.Zero)
        {
            _logger.LogDebug("Response delayed on {Delay:0.00}ms", value.Delay.TotalMilliseconds);
            await Task.Delay(value.Delay, cancellationToken);
        }

        return new ReadStubResponseQueryResult(
                Body: value.Response,
                Status: value.Status,
                Metadata: value.Metadata);
    }
}