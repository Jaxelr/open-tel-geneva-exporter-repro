using Microsoft.Extensions.Logging;

namespace open_tel_geneva_exporter_repro;

public static partial class SampleLogs
{
    [LoggerMessage
    (EventId = (int)LogEvents.ConsoleStarted,
     EventName = nameof(LogEvents.ConsoleStarted),
     Level = LogLevel.Information,
     Message = "{consoleName} Console started")]
    public static partial void ConsoleStarted(this ILogger logger, string? consoleName);

    [LoggerMessage
        (EventId = (int)LogEvents.ConsoleExited,
        EventName = nameof(LogEvents.ConsoleExited),
        Level = LogLevel.Information,
        Message = "{consoleName} Console exited")]
    public static partial void ConsoleExited(this ILogger logger, string? consoleName);
}

internal enum LogEvents
{
    ConsoleStarted = 1,
    ConsoleExited = 2
}