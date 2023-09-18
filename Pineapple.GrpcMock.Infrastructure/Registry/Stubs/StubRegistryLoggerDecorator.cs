using Microsoft.Extensions.Logging;
using Pineapple.GrpcMock.Application.Common.Registry;
using Pineapple.GrpcMock.Application.Stubs.Dto;
using Pineapple.GrpcMock.RpcHost.Shared.Helpers;

namespace Pineapple.GrpcMock.Infrastructure.Registry.Stubs;

internal sealed class StubRegistryLoggerDecorator : IStubRegistry
{
    private readonly IStubRegistry _registry;
    private readonly ILogger _logger;

    public StubRegistryLoggerDecorator(IStubRegistry registry, ILogger<StubRegistryLoggerDecorator> logger)
    {
        _registry = registry;
        _logger = logger;
    }

    public void Add(StubRegistryKeyDto key, StubRegistryValueDto value)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            _registry.Add(key, value);
            _logger.LogTrace("Add stub with key {@Key} and value {@Value} is completed in {Elapsed:0.0000}ms.", key, value, timer.GetElapsedTime().TotalMilliseconds);
        }
        catch
        {
            _logger.LogError("Add stub with key {@Key} and value {@Value} is failed in {Elapsed:0.0000}ms.", key, value, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }

    public IReadOnlyList<StubRegistryValueDto> Get(StubRegistryKeyDto key)
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            var value = _registry.Get(key);
            _logger.LogTrace("Get stub by key {@Key} is completed in {Elapsed:0.0000}ms.", key, timer.GetElapsedTime().TotalMilliseconds);
            return value;
        }
        catch
        {
            _logger.LogError("Get stub by key {@Key} is failed in {Elapsed:0.0000}ms.", key, timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }

    public IReadOnlyDictionary<StubRegistryKeyDto, IReadOnlyList<StubRegistryValueDto>> List()
    {
        var timer = ValueStopwatch.StartNew();
        try
        {
            var stubs = _registry.List();
            _logger.LogTrace("Get all stubs is completed in {Elapsed:0.0000}ms.", timer.GetElapsedTime().TotalMilliseconds);
            return stubs;
        }
        catch
        {
            _logger.LogError("Get all stubs is failed in {Elapsed:0.0000}ms.", timer.GetElapsedTime().TotalMilliseconds);
            throw;
        }
    }
}
