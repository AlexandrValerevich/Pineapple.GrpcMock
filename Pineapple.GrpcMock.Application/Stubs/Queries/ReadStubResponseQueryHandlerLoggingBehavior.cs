using ErrorOr;
using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

internal sealed class ReadStubResponseQueryHandlerLoggingBehavior : IPipelineBehavior<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>>
{
    private readonly ILogger _logger;

    public ReadStubResponseQueryHandlerLoggingBehavior(ILogger<ReadStubResponseQueryHandlerLoggingBehavior> logger)
    {
        _logger = logger;
    }

    public async ValueTask<ErrorOr<ReadStubResponseQueryResult>> Handle(ReadStubResponseQuery message, MessageHandlerDelegate<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>> next, CancellationToken cancellationToken)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _logger.LogTrace("Read stub response query for [{ServiceName}/{Method}].", message.ServiceFullName, message.Method);
            var response = await next(message, cancellationToken);
            response.Switch(
                value => _logger.LogInformation("Read stub response query for [{ServiceName}/{Method}] is completed in {Elapsed:0.0000}ms.",
                    message.ServiceFullName,
                    message.Method,
                    timer.GetElapsedTime().TotalMilliseconds),
                errors => _logger.LogError("Read stub response query for [{ServiceName}/{Method}] is failed in {Elapsed:0.0000}ms. {@Errors}",
                    message.ServiceFullName,
                    message.Method,
                    timer.GetElapsedTime().TotalMilliseconds,
                    errors)
            );

            return response;
        }
        catch
        {
            _logger.LogError("Read stub response query for [{ServiceName}/{Method}] is failed in {Elapsed:0.0000}ms.", message.ServiceFullName, message.Method, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}
