using ErrorOr;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAddStubCommand(this IServiceCollection services)
    {
        services.AddPipelineBehavior<AddStubCommand, ErrorOr<Unit>, AddStubCommandHandlerLoggingBehavior>();
        services.AddErrorOrValidationBehavior<AddStubCommand, Unit>();

        return services;
    }
}
