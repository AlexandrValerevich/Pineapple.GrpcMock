using Hellang.Middleware.ProblemDetails;
using Pineapple.GrpcMock.RpcHost.Configurations;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;
using Pineapple.GrpcMock.RpcHost.Services.Interceptors;

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

        services.AddGrpc(o =>
        {
            o.Interceptors.Add<ExceptionHandlingServerInterceptor>();
            o.Interceptors.Add<LoggingServerInterceptor>();
            o.Interceptors.Add<StubServerInterceptor>();
        });
        services.AddGrpcReflection();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddMinimalHttpServerLogger();
        ProblemDetailsExtensions.AddProblemDetails(services, o =>
        {
            o.IncludeExceptionDetails = (ctx, ex) => false;
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "GrpcMock Api", Version = "v1" });
        });

        return services;
    }
}