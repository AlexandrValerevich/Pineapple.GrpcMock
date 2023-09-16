using FluentValidation;

namespace Pineapple.GrpcMock.Application.Stubs.Queries;

internal sealed class ReadStubResponseQueryValidator : AbstractValidator<ReadStubResponseQuery>
{
    public ReadStubResponseQueryValidator()
    {
        RuleFor(x => x.ServiceFullName).NotEmpty();
        RuleFor(x => x.Method).NotEmpty();
        RuleFor(x => x.Request).NotNull();
    }
}
