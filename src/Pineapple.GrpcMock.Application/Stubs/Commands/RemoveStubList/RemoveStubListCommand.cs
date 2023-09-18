using ErrorOr;
using Mediator;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList;

public record RemoveStubListCommand(
    string ServiceShortName,
    string Method) : ICommand<ErrorOr<Unit>>;
