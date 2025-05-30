﻿using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace XunitTestLogger;

public class TestLogger<T> : ILogger<T>
{
	private readonly ILogger<T>? _dispatchTo;
	private readonly TestLogger _logger;

	public TestLogger(ITestOutputHelper helper, LogLevel mininumLogLevel = LogLevel.Information,
		ILogger<T>? dispatchTo = null)
	{
		_logger = new(helper, mininumLogLevel);
		_dispatchTo = dispatchTo;
	}

	public LogLevel MinimumLogLevel
	{
		get => _logger.MinimumLogLevel;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
		Func<TState, Exception?, string> formatter)
	{
		_dispatchTo?.Log(logLevel, eventId, state, exception, formatter);
		_logger.Log(logLevel, eventId, state, exception, formatter);
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return _logger.IsEnabled(logLevel);
	}

	public IDisposable? BeginScope<TState>(TState state) where TState : notnull
	{
		return _logger.BeginScope(state);
	}
}