using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

internal sealed class AddStubCommandHandler : ICommandHandler<AddStubCommand>
{
    public ValueTask<Unit> Handle(AddStubCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
