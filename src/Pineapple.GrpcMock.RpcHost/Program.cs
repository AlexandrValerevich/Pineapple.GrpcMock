using Hellang.Middleware.ProblemDetails;
using Pineapple.GrpcMock.Application;
using Pineapple.GrpcMock.Infrastructure;
using Pineapple.GrpcMock.RpcHost;
using Pineapple.GrpcMock.RpcHost.Extensions;
using Pineapple.GrpcMock.RpcHost.Host.Extensions;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;
using Pineapple.GrpcMock.RpcHost.Middlewares.TraceId.Extensions;
using Pineapple.GrpcMock.RpcHost.Rpc.Extensions;

#pragma warning disable

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseConfigurableSerilog();
    builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
    builder.WebHost.ConfigureKestrel();

    builder.Services.AddPresentation(
        builder.Configuration.GetSection("Stub").Bind
    );
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
}

var app = builder.Build();
{
    app.InitializeStubs();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseTraceIdHeaderMiddleware();

    app.MapGrpcStubServices();
    app.MapGrpcReflectionService();

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), b =>
    {
        b.UseMinimalHttpServerLogger();
        b.UseProblemDetails();
    });

    app.MapControllers();

    app.Run();
}