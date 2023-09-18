using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubList.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReadStubListQuery(this IServiceCollection services)
    {
        services.AddPipelineBehavior<ReadStubListQuery, ReadStubListQueryResult, ReadStubListQueryHandlerLoggingBehavior>();
        return services;
    }
}
