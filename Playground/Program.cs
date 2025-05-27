using Microsoft.Extensions.Logging;
using XunitTestLogger;
using XunitTestLoggerTests;

namespace Playground;

public static class MainEntry
{
	private static void Main()
	{
		var output = new DummyTestOutput();
		var helper = new DummyHelper(output);
		var logger = new TestLogger(helper, LogLevel.Trace);

		logger.LogError("Error");

		Console.WriteLine(helper.EscapedString);

	}
}