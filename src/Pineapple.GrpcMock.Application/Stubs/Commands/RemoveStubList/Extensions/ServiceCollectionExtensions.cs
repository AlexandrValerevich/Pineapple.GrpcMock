using ErrorOr;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRemoveStubListCommand(this IServiceCollection services)
    {
        services.AddPipelineBehavior<RemoveStubListCommand, ErrorOr<Unit>, RemoveStubListCommandHandlerLoggingBehavior>();
        services.AddValidationBehavior<RemoveStubListCommand, Unit>();

        return services;
    }
}
