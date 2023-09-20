using FluentValidation;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.ProtoMeta.Dto;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

internal sealed class AddStubCommandValidator : AbstractValidator<AddStubCommand>
{
    private readonly IProtoMetaRegistry _protoMetaRegistry;

    public AddStubCommandValidator(IProtoMetaRegistry protoMetaRegistry)
    {
        _protoMetaRegistry = protoMetaRegistry;

        RuleFor(x => x.Method)
            .NotEmpty()
            .WithMessage("Invalid Method Name");

        RuleFor(x => x.ServiceShortName)
            .NotEmpty()
            .WithMessage("Invalid Service Short Name");

        RuleFor(command => command)
            .NotNull()
            .Must((command) =>
            {
                var service = _protoMetaRegistry.Get(command.ServiceShortName);
                if (service is not null)
                {
                    IReadOnlyList<ProtoMethodMetaDto> methods = service.Methods;
                    return methods.Any(x => x.Name == command.Method);
                }

                return false;
            })
            .WithMessage(x => $"System not contain {x.ServiceShortName}/{x.Method}");

        RuleFor(x => x.Status).SetValidator(new StubStatusDtoValidator());
    }

}