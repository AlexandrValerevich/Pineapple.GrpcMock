using Microsoft.Extensions.Logging;

namespace Pineapple.GrpcMock.Shared.Interceptors;

internal sealed class LoggingClientInterceptorFactory : ILoggingClientInterceptorFactory
{
    private readonly Lazy<LoggingClientInterceptor> _loggingInterceptor;

    public LoggingClientInterceptorFactory(ILoggerFactory loggerFactory)
    {
        _loggingInterceptor = new Lazy<LoggingClientInterceptor>(() => new LoggingClientInterceptor(loggerFactory.CreateLogger<LoggingClientInterceptor>()));
    }

    public LoggingClientInterceptor Create()
    {
        return _loggingInterceptor.Value;
    }
}