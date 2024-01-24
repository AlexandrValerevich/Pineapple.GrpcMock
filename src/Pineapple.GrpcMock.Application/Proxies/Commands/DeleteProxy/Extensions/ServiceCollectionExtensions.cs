using ErrorOr;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.DeleteProxy.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeleteProxyCommand(this IServiceCollection services)
    {
        services.AddPipelineBehavior<DeleteProxyCommand, Unit, DeleteProxyCommandHandlerLoggingBehavior>();

        return services;
    }
}
