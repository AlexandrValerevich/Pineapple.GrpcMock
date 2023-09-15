namespace Pineapple.GrpcMock.RpcHost.Middlewares.TraceId.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseTraceIdHeaderMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<TraceIdHeaderMiddleware>();
    }
}