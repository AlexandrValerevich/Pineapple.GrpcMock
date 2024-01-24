using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;
using Pineapple.GrpcMock.Application.Proxies;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;

namespace Pineapple.GrpcMock.RpcHost.Proxies;

internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Map required proxy requests
    /// </summary>
    public static IApplicationBuilder MapRequiredProxy(this IApplicationBuilder app)
    {
        return app.MapWhen(
            ctx =>
            {
                IProxyService proxyService = ctx.RequestServices.GetRequiredService<IProxyService>();
                return proxyService.Can(new CanProxyQueryDto(ctx.Request.Path));
            },
            builder =>
            {
                builder.UseMinimalHttpServerLogger();
                builder.RunHttpProxy(
                    (ctx, arg) =>
                    {
                        return ctx.RequestServices.GetRequiredService<IProxyService>().GetUrl(new GetProxyUrlQueryDto(ctx.Request.Path));
                    },
                    http =>
                    {
                        http.WithBeforeSend((ctx, req) =>
                        {
                            var logger = ctx.RequestServices.GetRequiredService<ILogger<IHttpProxyOptionsBuilder>>();
                            logger.LogTrace("Request will be redirected to [{Redirect}]", req.RequestUri);
                            req.Version = new Version(2, 0);
                            return Task.CompletedTask;
                        });
                    }
                );
            }
        );
    }
}
