using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pineapple.GrpcMock.Application.Common.Converter;

namespace Pineapple.GrpcMock.Infrastructure.Converters.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddProtobufConverter(this IServiceCollection services)
    {
        services.TryAddSingleton<IProtobufConverter, ProtobufConverter>();
        return services;
    }
}
