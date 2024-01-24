using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList.Extensions;
using Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy.Extensions;
using Pineapple.GrpcMock.Application.Proxies.Commands.DeleteProxy.Extensions;
using Pineapple.GrpcMock.Application.Proxies.Extensions;
using Pineapple.GrpcMock.Application.Stubs.Commands.AddStub.Extensions;
using Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList.Extensions;
using Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList.Extensions;
using Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse.Extensions;

namespace Pineapple.GrpcMock.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton, includeInternalTypes: true);
        services.AddMediator();

        services.AddReadProtoMetaListQuery();

        services.AddAddOrUpdateProxyCommand();
        services.AddDeleteProxyCommand();

        services.AddAddStubCommand();
        services.AddRemoveStubListCommand();
        services.AddReadStubResponseQuery();
        services.AddReadStubListQuery();


        services.TryAddProxyService();
        return services;
    }
}