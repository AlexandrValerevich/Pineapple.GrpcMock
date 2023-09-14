using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry;
using Pineapple.GrpcMock.Application.Common.Stub.Registry;
using Pineapple.GrpcMock.Infrastructure.GrpcServices.Registry;
using Pineapple.GrpcMock.Infrastructure.Stubs.Registry;

namespace Pineapple.GrpcMock.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddSingleton<IStubRegistry, StubRegistry>();
        services.TryAddSingleton<IGrpcServicesRegistry, GrpcServicesRegistry>();
        return services;
    }
}
