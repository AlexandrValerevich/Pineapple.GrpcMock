using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.GrpcServices.Queries.ReadGrpcServiceList.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReadGrpcServiceListQuery(this IServiceCollection services)
    {
        services.AddPipelineBehavior<ReadGrpcServiceListQuery, ReadGrpcServiceListQueryResult, ReadGrpcServiceListQueryHandlerLoggingBehavior>();
        return services;
    }
}
