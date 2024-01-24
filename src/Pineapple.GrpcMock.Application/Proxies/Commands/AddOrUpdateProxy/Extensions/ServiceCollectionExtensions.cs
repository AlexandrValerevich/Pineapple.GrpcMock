using ErrorOr;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAddOrUpdateProxyCommand(this IServiceCollection services)
    {
        services.AddPipelineBehavior<AddOrUpdateProxyCommand, ErrorOr<Unit>, AddOrUpdateProxyCommandHandlerLoggingBehavior>();
        services.AddValidationBehavior<AddOrUpdateProxyCommand, Unit>();

        return services;
    }
}
