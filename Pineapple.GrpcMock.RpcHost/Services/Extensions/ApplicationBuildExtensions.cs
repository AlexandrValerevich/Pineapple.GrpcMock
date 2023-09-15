namespace Pineapple.GrpcMock.RpcHost.Services.Extensions;

internal static class ApplicationBuildExtensions
{
    public static IApplicationBuilder MapGrpcStubServices(this IApplicationBuilder app)
    {
        return app;
    }
}
