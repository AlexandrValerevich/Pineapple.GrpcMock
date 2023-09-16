using System.Reflection;
using System.Security.Cryptography;
using ErrorOr;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly(), includeInternalTypes: true);

        services.AddMediator();
        services.TryAddValidationBehavior();
        services.TryAddPipelineBehavior<AddStubCommand, ErrorOr<Unit>, AddStubCommandHandlerLoggingBehavior>();
        services.TryAddPipelineBehavior<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>, ReadStubResponseQueryHandlerLoggingBehavior>();
        services.TryAddPipelineBehavior<ReadGrpcServiceListQuery, ReadGrpcServiceListQueryResult, ReadGrpcServiceListQueryHandlerLoggingBehavior>();
        return services;
    }
}