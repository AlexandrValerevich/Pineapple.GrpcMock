using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList;

internal sealed class ReadStubListQueryHandlerLoggingBehavior : IPipelineBehavior<ReadStubListQuery, ReadStubListQueryResult>
{
    private readonly ILogger _logger;

    public ReadStubListQueryHandlerLoggingBehavior(ILogger<ReadStubListQueryHandlerLoggingBehavior> logger)
    {
        _logger = logger;
    }


    public async ValueTask<ReadStubListQueryResult> Handle(
        ReadStubListQuery message, MessageHandlerDelegate<ReadStubListQuery, ReadStubListQueryResult> next, CancellationToken cancellationToken)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _logger.LogTrace("Read stub list query.");
            var response = await next(message, cancellationToken);
            _logger.LogInformation("Read stub list query is completed in {Elapsed:0.0000}ms.", timer.GetElapsedTime().TotalMilliseconds);

            return response;
        }
        catch
        {
            _logger.LogError("Read stub list query is failed in {Elapsed:0.0000}ms.", timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}
