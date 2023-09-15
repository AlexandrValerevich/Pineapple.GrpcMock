using Pineapple.GrpcMock.Application;
using Pineapple.GrpcMock.Infrastructure;
using Pineapple.GrpcMock.RpcHost;
using Pineapple.GrpcMock.RpcHost.Extensions;
using Pineapple.GrpcMock.RpcHost.Host.Extensions;
using Pineapple.GrpcMock.RpcHost.Middlewares.ServerLogging.Extensions;
using Pineapple.GrpcMock.RpcHost.Middlewares.TraceId.Extensions;
using Pineapple.GrpcMock.RpcHost.Services.Extensions;

#pragma warning disable

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseConfigurableSerilog();
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

    app.UseMinimalHttpServerLogger();
    app.UseTraceIdHeaderMiddleware();

    app.MapGrpcStubServices();
    app.Run();
}