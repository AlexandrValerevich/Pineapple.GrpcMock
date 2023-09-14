using System.Text.Json;
using Google.Protobuf;
using Mediator;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry;
using Pineapple.GrpcMock.Application.Common.GrpcServices.Registry.Dto;
using Pineapple.GrpcMock.Application.Common.Stub.Registry;
using Pineapple.GrpcMock.Application.Common.Stub.Registry.Dto;

namespace Pineapple.GrpcMock.Application.Stubs.Commands;

internal sealed class AddStubCommandHandler : ICommandHandler<AddStubCommand>
{
    private readonly IStubRegistry _stubs;
    private readonly IGrpcServicesRegistry _grpcServices;

    public AddStubCommandHandler(IStubRegistry stubs, IGrpcServicesRegistry grpcServices)
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
            ShortServiceName: command.ServiceShortName,
            ServiceMethod: command.ServiceMethod,
            RequestBody: request.ToByteArray());

        var response = JsonSerializer.Deserialize(command.ResponseBody, method.OutputType) as Google.Protobuf.IMessage;
        var value = new StubRegistryValueDto(
            Response: response.ToByteArray());

        _stubs.Add(key, value);

        return ValueTask.FromResult(Unit.Value);
    }
}
