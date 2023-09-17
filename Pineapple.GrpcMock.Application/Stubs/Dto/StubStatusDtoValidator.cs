using FluentValidation;

namespace Pineapple.GrpcMock.Application.Stubs.Dto;

internal sealed class StubStatusDtoValidator : AbstractValidator<StubStatusDto>
{
    public StubStatusDtoValidator()
    {
        RuleFor(x => x.Code).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Details).NotNull();
    }
}
