using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Infrastructure.Registry.ProtoMeta.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddProtoMetaRegistry(this IServiceCollection services)
    {
        services.TryAddSingleton<IProtoMetaRegistry, ProtoMetaRegistry>();
        return services;
    }
}
