using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReadProtoMetaListQuery(this IServiceCollection services)
    {
        services.AddPipelineBehavior<ReadProtoMetaListQuery, ReadProtoMetaListQueryResult, ReadProtoMetaListQueryHandlerLoggingBehavior>();
        return services;
    }
}
