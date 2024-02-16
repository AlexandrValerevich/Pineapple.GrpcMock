
namespace Pineapple.GrpcMock.Shared.Interceptors;

internal interface ILoggingClientInterceptorFactory
{
    LoggingClientInterceptor Create();
}
