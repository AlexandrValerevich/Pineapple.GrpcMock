using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

public record ReadStubResponseQuery(
    string ServiceFullName,
    string Method,
    Google.Protobuf.IMessage Request) : IQuery<ReadStubResponseQueryResult>;