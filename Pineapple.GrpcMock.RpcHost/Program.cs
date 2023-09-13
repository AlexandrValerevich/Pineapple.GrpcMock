using Pineapple.GrpcMock.Application;
using Pineapple.GrpcMock.Infrastructure;
using Pineapple.GrpcMock.RpcHost;
using Pineapple.GrpcMock.RpcHost.Extensions;
using Pineapple.GrpcMock.RpcHost.Logging.Extensions;

#pragma warning disable

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseConfigurableSerilog();

    builder.Services.AddPresentation(
        builder.Configuration.GetSection("Stub").Bind
    );
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
}

var app = builder.Build();
{
    app.SetUpStubs();

    app.UseMinimalHttpServerLogger();
    app.MapControllers();
    app.Run();
}