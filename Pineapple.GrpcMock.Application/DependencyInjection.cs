using ErrorOr;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;
using Pineapple.GrpcMock.Application.GrpcServices.Queries;
using Pineapple.GrpcMock.Application.Stubs.Commands;
using Pineapple.GrpcMock.Application.Stubs.Queries;

namespace Pineapple.GrpcMock.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();
        services.TryAddPipelineBehavior<AddStubCommand, Unit, AddStubCommandHandlerLoggingBehavior>();
        services.TryAddPipelineBehavior<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>, ReadStubResponseQueryHandlerLoggingBehavior>();
        services.TryAddPipelineBehavior<ReadGrpcServiceListQuery, ReadGrpcServiceListQueryResult, ReadGrpcServiceListQueryHandlerLoggingBehavior>();
        return services;
    }
}