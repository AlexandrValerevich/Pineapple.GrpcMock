using System.Reflection;
using Pineapple.GrpcMock.Application;
using Pineapple.GrpcMock.Infrastructure;
using Pineapple.GrpcMock.RpcHost;
using Pineapple.GrpcMock.RpcHost.Configurations;
using Pineapple.GrpcMock.RpcHost.Extensions;

#pragma warning disable

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(
        builder.Configuration.GetSection("Stub").Bind
    );
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
}

var app = builder.Build();
{
    app.SetUpStubs();
    app.MapGet("/", () => "Hello World!");
    app.Run();
}