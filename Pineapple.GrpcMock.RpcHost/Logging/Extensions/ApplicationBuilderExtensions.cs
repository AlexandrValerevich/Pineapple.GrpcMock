namespace Pineapple.GrpcMock.RpcHost.Logging.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMinimalHttpServerLogger(this IApplicationBuilder app)
    {
        return app.UseMiddleware<HttpServerLoggerMiddleware>();
    }
}