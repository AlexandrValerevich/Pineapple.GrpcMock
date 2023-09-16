using ErrorOr;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Mediator;
using Pineapple.GrpcMock.Application.Stubs.Queries;
using Pineapple.GrpcMock.RpcHost.Services.Interceptors.Extensions;

namespace Pineapple.GrpcMock.RpcHost.Services.Interceptors;

internal sealed class StubInterceptor : Interceptor
{
    private readonly IMediator _mediator;

    public StubInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        string[] splitMethod = context.Method[1..].Split('/');
        ErrorOr<ReadStubResponseQueryResult> result = await _mediator.Send(new ReadStubResponseQuery(
            ServiceFullName: splitMethod[0],
            Method: splitMethod[1],
            Request: (Google.Protobuf.IMessage) request));

        return result.MatchFirst(
            value => (TResponse) value.Response,
            error => throw new RpcException(new Status(error.ToStatusCode(), error.Description))
        );
    }
}