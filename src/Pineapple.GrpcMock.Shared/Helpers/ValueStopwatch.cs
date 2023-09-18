using System.Diagnostics;

namespace Pineapple.GrpcMock.RpcHost.Shared.Helpers;

/// <summary>
/// Lightweight stopwatch timer.
/// </summary>
internal readonly struct ValueStopwatch
{
    private static readonly double _timestampToTicks = TimeSpan.TicksPerSecond / (double) Stopwatch.Frequency;

    private readonly long _startTimestamp;

    public bool IsActive => _startTimestamp != 0;

    private ValueStopwatch(long startTimestamp)
    {
        _startTimestamp = startTimestamp;
    }

    public static ValueStopwatch StartNew() => new(Stopwatch.GetTimestamp());

    public TimeSpan GetElapsedTime()
    {
        if (!IsActive)
            throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");

        var end = Stopwatch.GetTimestamp();
        var timestampDelta = end - _startTimestamp;
        var ticks = (long) (_timestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
    }
}