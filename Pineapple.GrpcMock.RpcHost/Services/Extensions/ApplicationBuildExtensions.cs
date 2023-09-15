using System.Reflection;
using Mediator;
using Pineapple.GrpcMock.Application.GrpcServices.Dto;
using Pineapple.GrpcMock.Application.GrpcServices.Queries;
using Throw;

namespace Pineapple.GrpcMock.RpcHost.Services.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGrpcStubServices(this IEndpointRouteBuilder builder)
    {
        var mediator = builder.ServiceProvider.GetRequiredService<IMediator>();
        Task<ReadGrpcServiceListQueryResult> metaTask = mediator.Send(ReadGrpcServiceListQuery.Instance).AsTask();
        metaTask.Wait();

        ReadGrpcServiceListQueryResult meta = metaTask.Result;
        MethodInfo extensionsMethod = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod(
            nameof(GrpcEndpointRouteBuilderExtensions.MapGrpcService)).ThrowIfNull();

        foreach (GrpcServiceMetaDto service in meta.ServicesMeta)
            extensionsMethod.MakeGenericMethod(service.ServiceType).Invoke(null, new object[] { builder });

        return builder;
    }
}