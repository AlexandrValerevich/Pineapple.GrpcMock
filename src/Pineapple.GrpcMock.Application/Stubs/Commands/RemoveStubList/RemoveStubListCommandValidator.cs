using FluentValidation;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList;

internal sealed class RemoveStubListCommandValidator : AbstractValidator<RemoveStubListCommand>
{
    public RemoveStubListCommandValidator()
    {
        RuleFor(x => x.Method).NotEmpty();
        RuleFor(x => x.ServiceShortName).NotEmpty();
    }
}
