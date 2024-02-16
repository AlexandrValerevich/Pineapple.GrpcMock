
using Microsoft.Extensions.DependencyInjection;

namespace Pineapple.GrpcMock.Shared.ServiceCollection;

internal static class ServiceCollectionExtensions
{
    public static bool Contain<T>(this IServiceCollection services)
    {
        return services.Any(x => x.ServiceType == typeof(T));
    }
}
