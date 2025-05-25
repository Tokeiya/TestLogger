using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using XunitTestLogger;

namespace XunitTestLoggerTests;

using Xunit;
using FakeItEasy;


public class UnitTest1
{
	[Fact]
	public void CtorTest()
	{
		var dummy = A.Dummy<ITestOutputHelper>();

		TestLogger fixtire = new(dummy);
		fixtire.DefaultBackgroundColor.Is(ConsoleColor.Black);
		fixtire.DefaultForegroundColor.Is(ConsoleColor.Gray);
		fixtire.MinimumLogLevel.Is(LogLevel.Information);

		fixtire=new TestLogger(dummy,LogLevel.Debug);
		fixtire.DefaultBackgroundColor.Is(ConsoleColor.Black);
		fixtire.DefaultForegroundColor.Is(ConsoleColor.Gray);
		fixtire.MinimumLogLevel.Is(LogLevel.Debug);
		
		fixtire=new TestLogger(dummy,LogLevel.Debug,ConsoleColor.Red,ConsoleColor.Green);
		fixtire.DefaultBackgroundColor.Is(ConsoleColor.Green);
		fixtire.DefaultForegroundColor.Is(ConsoleColor.Red);
		fixtire.MinimumLogLevel.Is(LogLevel.Debug);
	}
}