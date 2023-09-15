using Google.Protobuf;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

public record ReadStubResponseQueryResult(
    IMessage Response);