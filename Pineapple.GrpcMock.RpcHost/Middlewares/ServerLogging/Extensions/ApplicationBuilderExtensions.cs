namespace Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMinimalHttpServerLogger(this IApplicationBuilder app)
    {
        return app.UseMiddleware<HttpServerLoggerMiddleware>();
    }
}