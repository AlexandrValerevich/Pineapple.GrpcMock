using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Infrastructure.Registry.Stubs.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddStubRegistry(this IServiceCollection services)
    {
        if (services.Any(x => x.ServiceType == typeof(IStubRegistry)))
            return services;

        services.AddSingleton<IStubRegistry, StubRegistry>();
        services.Decorate<IStubRegistry, StubRegistryLoggerDecorator>();

        return services;
    }
}
