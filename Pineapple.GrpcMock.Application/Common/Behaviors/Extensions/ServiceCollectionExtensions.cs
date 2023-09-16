using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection TryAddPipelineBehavior<TRequest, TResponse, TBehavior>(this IServiceCollection services)
        where TRequest : notnull, IMessage
        where TBehavior : class, IPipelineBehavior<TRequest, TResponse>
    {
        services.TryAddSingleton<IPipelineBehavior<TRequest, TResponse>, TBehavior>();
        return services;
    }

    public static IServiceCollection TryAddValidationBehavior(this IServiceCollection services)
    {
        services.TryAddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
