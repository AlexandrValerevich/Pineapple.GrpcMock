using ErrorOr;
using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy;

internal sealed class AddOrUpdateProxyCommandHandlerLoggingBehavior : IPipelineBehavior<AddOrUpdateProxyCommand, ErrorOr<Unit>>
{
     private readonly ILogger _logger;

    public AddOrUpdateProxyCommandHandlerLoggingBehavior(ILogger<AddOrUpdateProxyCommandHandlerLoggingBehavior> logger)
    {
        _logger = logger;
    }

    public async ValueTask<ErrorOr<Unit>> Handle(AddOrUpdateProxyCommand message, MessageHandlerDelegate<AddOrUpdateProxyCommand, ErrorOr<Unit>> next, CancellationToken cancellationToken)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _logger.LogTrace("Add or update proxy to [{Url}] command for [{ServiceName}].",message.ProxyUrl, message.ServiceShortName);

            var response = await next(message, cancellationToken);
            response.Switch(
                value => _logger.LogInformation("Add or update proxy to [{Url}] command for [{ServiceName}] is completed in {Elapsed:0.0000}ms.", message.ProxyUrl, message.ServiceShortName, timer.GetElapsedTime().TotalMilliseconds),
                errors => _logger.LogError("Add or update proxy to [{Url}] command for [{ServiceName}] is failed in {Elapsed:0.0000}ms. {@Errors}", message.ProxyUrl, message.ServiceShortName, timer.GetElapsedTime().TotalMilliseconds, errors)
            );

            return response;
        }
        catch
        {
            _logger.LogError("Add or update proxy to [{Url}] command for [{ServiceName}] is failed in {Elapsed:0.0000}ms.", message.ProxyUrl, message.ServiceShortName, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}