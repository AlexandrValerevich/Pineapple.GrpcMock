using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Infrastructure.Converters.Extensions;
using Pineapple.GrpcMock.Infrastructure.Registry.GrpcServices.Extensions;
using Pineapple.GrpcMock.Infrastructure.Registry.Stubs.Extensions;

namespace Pineapple.GrpcMock.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddProtobufConverter();
        services.TryAddStubRegistry();
        services.TryAddGrpcServiceRegistry();
        return services;
    }
}