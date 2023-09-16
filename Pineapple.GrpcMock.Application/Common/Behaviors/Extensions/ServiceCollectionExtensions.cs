using ErrorOr;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPipelineBehavior<TRequest, TResponse, TBehavior>(this IServiceCollection services)
        where TRequest : notnull, IMessage
        where TBehavior : class, IPipelineBehavior<TRequest, TResponse>
    {
        services.AddSingleton<IPipelineBehavior<TRequest, TResponse>, TBehavior>();
        return services;
    }

    public static IServiceCollection AddValidationBehavior<TRequest, TResponse>(this IServiceCollection services)
        where TRequest : notnull, IMessage
    {
        services.AddPipelineBehavior<TRequest, ErrorOr<TResponse>, ValidationBehavior<TRequest, ErrorOr<TResponse>>>();
        return services;
    }
}
