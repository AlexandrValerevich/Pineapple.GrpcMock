using Grpc.Core;
using Mediator;
using OneOf;
using OneOf.Types;

namespace Pineapple.GrpcMock.Application.Stubs.Queries.ReadStubResponse;

public record ReadStubResponseQuery(
    string ServiceFullName,
    string Method,
    Google.Protobuf.IMessage Request) : IQuery<OneOf<ReadStubResponseQueryResult, RpcException, NotFound>>;