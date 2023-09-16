using Mediator;
using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

internal sealed class AddStubCommandHandlerLoggingBehavior : IPipelineBehavior<AddStubCommand, Unit>
{
    private readonly ILogger _logger;

    public AddStubCommandHandlerLoggingBehavior(ILogger<AddStubCommandHandlerLoggingBehavior> logger)
    {
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(AddStubCommand message, MessageHandlerDelegate<AddStubCommand, Unit> next, CancellationToken cancellationToken)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _logger.LogTrace("Add stub command for [{ServiceName}/{Method}].", message.ServiceShortName, message.Method);
            var response = await next(message, cancellationToken);
            _logger.LogInformation("Add stub command for [{ServiceName}/{Method}] is completed in {Elapsed:0.0000}ms.", message.ServiceShortName, message.Method, timer.GetElapsedTime().TotalMilliseconds);

            return response;
        }
        catch
        {
            _logger.LogError("Add stub command for [{ServiceName}/{Method}] is failed in {Elapsed:0.0000}ms.", message.ServiceShortName, message.Method, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}
