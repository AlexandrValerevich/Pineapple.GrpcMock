using System.Reflection;
using Mediator;
using Pineapple.GrpcMock.Application.ProtoMeta.Dto;
using Pineapple.GrpcMock.Application.ProtoMeta.Queries.ReadProtoMetaList;
using Throw;

namespace Pineapple.GrpcMock.RpcHost.Rpc.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// This method register all grpc services that exist in Assembly.
    /// </summary>
    public static IEndpointRouteBuilder MapGrpcStubServices(this IEndpointRouteBuilder builder)
    {
        var mediator = builder.ServiceProvider.GetRequiredService<IMediator>();
        Task<ReadProtoMetaListQueryResult> metaTask = mediator.Send(ReadProtoMetaListQuery.Instance).AsTask();
        metaTask.Wait();

        ReadProtoMetaListQueryResult meta = metaTask.Result;
        MethodInfo extensionsMethod = typeof(GrpcEndpointRouteBuilderExtensions)
            .GetMethod(nameof(GrpcEndpointRouteBuilderExtensions.MapGrpcService))
            .ThrowIfNull();

        foreach (ProtoServiceMetaDto service in meta.ServicesMeta)
            extensionsMethod.MakeGenericMethod(service.ServiceType).Invoke(null, new object[] { builder });

        return builder;
    }
}