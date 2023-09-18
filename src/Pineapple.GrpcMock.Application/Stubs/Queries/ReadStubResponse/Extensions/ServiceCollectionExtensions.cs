using ErrorOr;
using Microsoft.Extensions.DependencyInjection;
using Pineapple.GrpcMock.Application.Common.Behaviors.Extensions;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReadStubResponseQuery(this IServiceCollection services)
    {
        services.AddPipelineBehavior<ReadStubResponseQuery, ErrorOr<ReadStubResponseQueryResult>, ReadStubResponseQueryHandlerLoggingBehavior>();
        services.AddValidationBehavior<ReadStubResponseQuery, ReadStubResponseQueryResult>();
        return services;
    }
}
