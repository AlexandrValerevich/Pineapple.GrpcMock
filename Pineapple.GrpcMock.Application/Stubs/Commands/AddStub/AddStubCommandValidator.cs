using System.Text.Json;
using FluentValidation;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

internal sealed class AddStubCommandValidator : AbstractValidator<AddStubCommand>
{
    public AddStubCommandValidator()
    {
        RuleFor(x => x.Method).NotEmpty();
        RuleFor(x => x.ServiceShortName).NotEmpty();
        RuleFor(x => x.RequestBody).NotEmpty().Must(BeValidJson);
        RuleFor(x => x.ResponseBody).NotEmpty().Must(BeValidJson);
        RuleFor(x => x.Status).SetValidator(new StubStatusDtoValidator());
    }

    private bool BeValidJson(string json)
    {
        try
        {
            // Attempt to deserialize the string as JSON
            JsonSerializer.Deserialize<object>(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
