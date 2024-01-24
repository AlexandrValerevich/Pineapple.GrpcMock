using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.DeleteProxy;

internal sealed class DeleteProxyCommandHandlerLoggingBehavior : IPipelineBehavior<DeleteProxyCommand, Unit>
{
     private readonly ILogger _logger;

    public DeleteProxyCommandHandlerLoggingBehavior(ILogger<DeleteProxyCommandHandlerLoggingBehavior> logger)
    {
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(DeleteProxyCommand message, MessageHandlerDelegate<DeleteProxyCommand, Unit> next, CancellationToken cancellationToken)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _logger.LogTrace("Delete proxy command for [{ServiceName}].", message.ServiceShortName);
            var response = await next(message, cancellationToken);
            _logger.LogInformation("Delete proxy command for [{ServiceName}] is completed in {Elapsed:0.0000}ms.", message.ServiceShortName, timer.GetElapsedTime().TotalMilliseconds);
            return response;
        }
        catch
        {
            _logger.LogError("Delete proxy command for [{ServiceName}] is failed in {Elapsed:0.0000}ms.", message.ServiceShortName, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}