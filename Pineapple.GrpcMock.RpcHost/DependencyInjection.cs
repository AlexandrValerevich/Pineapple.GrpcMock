using LigaStavok.Feed.BaseLine.Api.Middleware.Extensions;
using Pineapple.GrpcMock.RpcHost.Configurations;

namespace Pineapple.GrpcMock.RpcHost;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services,
        Action<StubOptions> stubOptions)
    {
        services.AddOptions<StubOptions>()
            .Configure(stubOptions)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMinimalHttpServerLogger();
        services.AddControllers();
        return services;
    }
}
