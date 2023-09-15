using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.GrpcServices.Registry;
using Pineapple.GrpcMock.Infrastructure.GrpcServices.Registry;
using Pineapple.GrpcMock.Infrastructure.Stubs.Registry.Extensions;

namespace Pineapple.GrpcMock.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddStubRegistry();
        services.TryAddSingleton<IGrpcServiceRegistry, GrpcServicesRegistry>();
        return services;
    }
}