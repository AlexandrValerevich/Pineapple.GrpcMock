using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList.Extensions;
using Pineapple.GrpcMock.Application.Stubs.Commands.AddStub.Extensions;
using Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse.Extensions;

namespace Pineapple.GrpcMock.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton, includeInternalTypes: true);
        services.AddMediator();
        services.AddReadStubResponseQuery();
        services.AddReadProtoMetaListQuery();
        services.AddAddStubCommand();
        return services;
    }
}