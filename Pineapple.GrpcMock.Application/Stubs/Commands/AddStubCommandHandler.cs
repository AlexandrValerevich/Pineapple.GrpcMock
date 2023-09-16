using System.Text.Json;
using Mediator;
using Pineapple.GrpcMock.Application.Common.Converter;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.GrpcServices.Dto;
using Pineapple.GrpcMock.Application.Stubs.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

internal sealed class AddStubCommandHandler : ICommandHandler<AddStubCommand>
{
    private readonly IStubRegistry _stubs;
    private readonly IGrpcServiceRegistry _grpcServices;
    private readonly IProtobufConverter _converter;

    public AddStubCommandHandler(IStubRegistry stubs, IGrpcServiceRegistry grpcServices, IProtobufConverter converter)
    {
        _stubs = stubs;
        _grpcServices = grpcServices;
        _converter = converter;
    }

    public ValueTask<Unit> Handle(AddStubCommand command, CancellationToken cancellationToken)
    {
        GrpcServiceMetaDto? service = _grpcServices.Get(command.ServiceShortName);
        if (service is null)
            return ValueTask.FromResult(Unit.Value);

        GrpcServiceMethodMetaDto? method = service.Methods.SingleOrDefault(x => x.Name == command.Method);
        if (method is null)
            return ValueTask.FromResult(Unit.Value);

        var key = new StubRegistryKeyDto(
            ServiceShortName: command.ServiceShortName,
            Method: command.Method);

        var value = new StubRegistryValueDto(
            Request: _converter.FromJson(method.InputType, command.RequestBody),
            Response: _converter.FromJson(method.OutputType, command.ResponseBody));

        _stubs.Add(key, value);

        return ValueTask.FromResult(Unit.Value);
    }
}