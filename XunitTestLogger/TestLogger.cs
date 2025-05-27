using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace XunitTestLogger;

public sealed class TestLogger : ILogger
{
	private const int DefaultForegroundColor = 39;
	private const int DefaultBackgroundColor = 49;

	private static readonly IReadOnlyDictionary<LogLevel, (int foreground, int background)> ColorSet;


	private static readonly IReadOnlyDictionary<ConsoleColor, int> ForegroundColors = new Dictionary<ConsoleColor, int>
	{
		[ConsoleColor.Black] = 30,
		[ConsoleColor.DarkRed] = 31,
		[ConsoleColor.DarkGreen] = 32,
		[ConsoleColor.DarkYellow] = 33,
		[ConsoleColor.DarkBlue] = 34,
		[ConsoleColor.DarkMagenta] = 35,
		[ConsoleColor.DarkCyan] = 36,
		[ConsoleColor.DarkGray] = 90,
		[ConsoleColor.Gray] = 37,
		[ConsoleColor.Red] = 91,
		[ConsoleColor.Green] = 92,
		[ConsoleColor.Yellow] = 93,
		[ConsoleColor.Blue] = 94,
		[ConsoleColor.Magenta] = 95,
		[ConsoleColor.Cyan] = 96,
		[ConsoleColor.White] = 97
	};


	private static readonly IReadOnlyDictionary<ConsoleColor, int> BackgroundColors = new Dictionary<ConsoleColor, int>
	{
		[ConsoleColor.Black] = 40,
		[ConsoleColor.DarkRed] = 41,
		[ConsoleColor.DarkGreen] = 42,
		[ConsoleColor.DarkYellow] = 43,
		[ConsoleColor.DarkBlue] = 44,
		[ConsoleColor.DarkMagenta] = 45,
		[ConsoleColor.DarkCyan] = 46,
		[ConsoleColor.DarkGray] = 100,
		[ConsoleColor.Gray] = 47,
		[ConsoleColor.Red] = 101,
		[ConsoleColor.Green] = 102,
		[ConsoleColor.Yellow] = 103,
		[ConsoleColor.Blue] = 104,
		[ConsoleColor.Magenta] = 105,
		[ConsoleColor.Cyan] = 106,
		[ConsoleColor.White] = 107
	};

	private readonly ILogger? _dispatchTo;

	private readonly ITestOutputHelper _helper;

	private readonly Lock _lock = new();

	static TestLogger()
	{
		ColorSet = new Dictionary<LogLevel, (int foreground, int background)>
		{
			[LogLevel.Debug] = (ForegroundColors[ConsoleColor.Cyan], DefaultBackgroundColor),
			[LogLevel.Trace] = (ForegroundColors[ConsoleColor.Blue], DefaultBackgroundColor),
			[LogLevel.Information] = (ForegroundColors[ConsoleColor.Green], DefaultBackgroundColor),
			[LogLevel.Warning] = (ForegroundColors[ConsoleColor.White], BackgroundColors[ConsoleColor.DarkYellow]),
			[LogLevel.Error] = (ForegroundColors[ConsoleColor.White], BackgroundColors[ConsoleColor.DarkMagenta]),
			[LogLevel.Critical] = (ForegroundColors[ConsoleColor.White], BackgroundColors[ConsoleColor.DarkRed])
		};
	}

	public TestLogger(ITestOutputHelper helper, LogLevel mininumLogLevel = LogLevel.Information,
		ILogger? dispatchTo = null)
	{
		_helper = helper;
		MinimumLogLevel = mininumLogLevel;
		_dispatchTo = dispatchTo;
	}

	public LogLevel MinimumLogLevel { get; }


	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
		Func<TState, Exception?, string> formatter)
	{
		_dispatchTo?.Log(logLevel, eventId, state, exception, formatter);


		if (IsEnabled(logLevel))
			lock (_lock)
			{
				var bld = new StringBuilder();

				SetColor(bld, ColorSet[logLevel]);
				bld.Append(logLevel);
				ResetColor(bld);
				bld.Append('\n');

				bld.Append(eventId);
				bld.Append('\n');

				bld.Append(formatter(state, exception));
				bld.Append('\n');

				if (exception is not null)
				{
					SetColor(bld, ConsoleColor.White, ConsoleColor.Red);
					bld.Append(exception.GetType().Name);
					ResetColor(bld);
					bld.Append('\n');
					bld.Append(exception.Message);
					bld.Append('\n');
					bld.Append(exception.StackTrace);
				}

				bld.Append('\n');

				_helper.WriteLine(bld.ToString());
			}
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return logLevel >= MinimumLogLevel;
	}

	public IDisposable? BeginScope<TState>(TState state) where TState : notnull
	{
		return null;
	}

	private static void SetColor(StringBuilder builder, ConsoleColor foreground, ConsoleColor background)
	{
		builder.Append($"\x1b[{ForegroundColors[foreground]};{BackgroundColors[background]}m");
	}

	private static void SetColor(StringBuilder builder, (int foreground, int background) tuple)
	{
		builder.Append($"\x1b[{tuple.foreground};{tuple.background}m");
	}

	private void ResetColor(StringBuilder builder)
	{
		builder.Append($"\x1b[{DefaultForegroundColor};{DefaultBackgroundColor}m");
	}
}