using Google.Protobuf;

namespace Pineapple.GrpcMock.Application.Common.Converter;

public interface IProtobufConverter
{
    IMessage FromJson(Type protoType, string json);
    string ToJson(IMessage proto);
}
