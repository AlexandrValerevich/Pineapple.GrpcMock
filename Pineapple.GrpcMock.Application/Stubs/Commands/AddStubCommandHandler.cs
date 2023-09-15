using System.Text.Json;
using Mediator;
using Pineapple.GrpcMock.Application.GrpcServices.Dto;
using Pineapple.GrpcMock.Application.GrpcServices.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Pineapple.GrpcMock.Application.Stubs.Registry;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

internal sealed class AddStubCommandHandler : ICommandHandler<AddStubCommand>
{
    private readonly IStubRegistry _stubs;
    private readonly IGrpcServiceRegistry _grpcServices;

    public AddStubCommandHandler(IStubRegistry stubs, IGrpcServiceRegistry grpcServices)
    {
        _stubs = stubs;
        _grpcServices = grpcServices;
    }

    public ValueTask<Unit> Handle(AddStubCommand command, CancellationToken cancellationToken)
    {
        GrpcServiceMetaDto? service = _grpcServices.Get(command.ServiceShortName);
        if (service is null)
            return ValueTask.FromResult(Unit.Value);

        GrpcServiceMethodMetaDto? method = service.Methods.SingleOrDefault(x => x.Name == command.ServiceMethod);
        if (method is null)
            return ValueTask.FromResult(Unit.Value);

        var request = JsonSerializer.Deserialize(command.RequestBody, method.InputType) as Google.Protobuf.IMessage;
        var key = new StubRegistryKeyDto(
            ServiceShortName: command.ServiceShortName,
            Method: command.ServiceMethod);

        var response = JsonSerializer.Deserialize(command.ResponseBody, method.OutputType) as Google.Protobuf.IMessage;
        var value = new StubRegistryValueDto(
            Request: request ?? throw new NullReferenceException(),
            Response: response ?? throw new NullReferenceException());

        _stubs.Add(key, value);

        return ValueTask.FromResult(Unit.Value);
    }
}
