namespace Pineapple.GrpcMock.RpcHost.Host.Configurations;

public class KestrelListenerOptions
{
    public ListenerOptions Http1 { get; set; } = new ListenerOptions
    {
        Port = 5001
    };

    public ListenerOptions Http2 { get; set; } = new ListenerOptions
    {
        Port = 5002
    };
}