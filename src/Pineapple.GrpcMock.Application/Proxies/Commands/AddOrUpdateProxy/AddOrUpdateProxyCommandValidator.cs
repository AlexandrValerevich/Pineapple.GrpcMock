using FluentValidation;
using Pineapple.GrpcMock.Application.Common.Registry;

namespace Pineapple.GrpcMock.Application.Proxies.Commands.AddOrUpdateProxy;

internal sealed class AddOrUpdateProxyCommandValidator : AbstractValidator<AddOrUpdateProxyCommand>
{
    private readonly IProtoMetaRegistry _protoMetaRegistry;

    public AddOrUpdateProxyCommandValidator(IProtoMetaRegistry protoMetaRegistry)
    {
        _protoMetaRegistry = protoMetaRegistry;
        RuleFor(x=> x.ProxyUrl)
            .NotEmpty()
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));

        RuleFor(x => x.ServiceShortName)
            .NotEmpty()
            .WithMessage("Invalid Service Short Name")
            .Must(serviceName => _protoMetaRegistry.Get(serviceName) is not null)
            .WithMessage(x => $"System not contain service {x.ServiceShortName}");
    }
}