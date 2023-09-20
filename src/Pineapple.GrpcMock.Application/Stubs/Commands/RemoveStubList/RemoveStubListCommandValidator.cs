using FluentValidation;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.ProtoMeta.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.RemoveStubList;

internal sealed class RemoveStubListCommandValidator : AbstractValidator<RemoveStubListCommand>
{
    private readonly IProtoMetaRegistry _protoMetaRegistry;

    public RemoveStubListCommandValidator(IProtoMetaRegistry protoMetaRegistry)
    {
        _protoMetaRegistry = protoMetaRegistry;

        RuleFor(x => x.Method).NotEmpty();
        RuleFor(x => x.ServiceShortName).NotEmpty();

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
    }
}
