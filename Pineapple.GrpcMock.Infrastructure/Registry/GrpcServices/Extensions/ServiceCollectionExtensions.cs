using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Infrastructure.Registry.GrpcServices.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddGrpcServiceRegistry(this IServiceCollection services)
    {
        services.TryAddSingleton<IGrpcServiceRegistry, GrpcServiceRegistry>();
        return services;
    }
}
