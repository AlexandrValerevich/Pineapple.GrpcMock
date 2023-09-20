using ErrorOr;
using Grpc.Core;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Converter;
using Pineapple.GrpcMock.Application.Common.Extensions;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.ProtoMeta.Dto;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Throw;

namespace Pineapple.GrpcMock.Application.Stubs.Commands.AddStub;

internal sealed class AddStubCommandHandler : ICommandHandler<AddStubCommand, ErrorOr<Unit>>
{
    private readonly IStubRegistry _stubs;
    private readonly IProtoMetaRegistry _protoMeta;
    private readonly IProtobufConverter _converter;

    public AddStubCommandHandler(IStubRegistry stubs, IProtoMetaRegistry protoMeta, IProtobufConverter converter)
    {
        _stubs = stubs;
        _protoMeta = protoMeta;
        _converter = converter;
    }

    public ValueTask<ErrorOr<Unit>> Handle(AddStubCommand command, CancellationToken cancellationToken)
    {
        ProtoServiceMetaDto service = _protoMeta.Get(command.ServiceShortName).ThrowIfNull("The existence of the service is not validated.");
        ProtoMethodMetaDto method = service.Methods.Single(x => x.Name == command.Method);

        var key = new StubRegistryKeyDto(
            ServiceShortName: command.ServiceShortName,
            Method: command.Method);

        var value = new StubRegistryValueDto(
            Request: _converter.FromJson(command.RequestBody, method.InputType),
            Response: _converter.FromJson(command.ResponseBody, method.OutputType),
            Status: new Status((StatusCode) command.Status.Code, command.Status.Details),
            Metadata: new Metadata().Add(command.Metadata.Trailer),
            Delay: command.Delay,
            Priority: command.Priority);

        _stubs.Add(key, value);

        return ValueTask.FromResult(ErrorOrFactory.From(Unit.Value));
    }
}