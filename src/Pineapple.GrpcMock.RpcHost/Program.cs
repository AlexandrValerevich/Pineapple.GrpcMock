using Hellang.Middleware.ProblemDetails;
using Pineapple.GrpcMock.Application;
using Pineapple.GrpcMock.Infrastructure;
using Pineapple.GrpcMock.RpcHost;
using Pineapple.GrpcMock.RpcHost.Host.Extensions;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;
using Pineapple.GrpcMock.RpcHost.Middlewares.TraceId.Extensions;
using Pineapple.GrpcMock.RpcHost.Proxies;
using Pineapple.GrpcMock.RpcHost.Rpc.Extensions;

#pragma warning disable

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseConfigurableSerilog();
    builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
    builder.WebHost.ConfigureKestrel();

    builder.Services.AddPresentation(builder.Configuration.GetSection("Stub").Bind);
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseTraceIdHeaderMiddleware();

    app.UseRouting();
    app.MapWhen(context => context.Request.Path.StartsWithSegments("/api"), b =>
    {
        b.UseMinimalHttpServerLogger();
        b.UseProblemDetails();
        b.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    });

    app.MapRequiredProxy();
    app.MapGrpcStubServices();
    app.MapGrpcReflectionService();

    app.Run();
}