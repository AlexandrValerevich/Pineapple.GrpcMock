using System.Text.Json;
using Google.Protobuf;

namespace Pineapple.GrpcMock.Application.Common.Converter;

public interface IProtobufConverter
{
    IMessage FromJson(JsonElement json, Type protoType);
    JsonElement ToJsonElement(IMessage proto);
}
