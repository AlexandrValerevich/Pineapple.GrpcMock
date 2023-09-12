using System.Reflection;
using Grpc.Core;

#pragma warning disable

var builder = WebApplication.CreateBuilder(args);
{
    var assembly = Assembly.GetExecutingAssembly();
    var grpcServiceTypes = assembly.GetTypes().Where(x => x.GetCustomAttributes<BindServiceMethodAttribute>(true).Any());
}

var app = builder.Build();
{
    app.MapGet("/", () => "Hello World!");
    app.Run();
}