using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using XunitTestLogger;

namespace XunitTestLoggerTests;

using Xunit;
using FakeItEasy;

class Foo : ITestOutputHelper
{
	private readonly ITestOutputHelper _underlying;
	
	public string Recent { get;private set; }
	
	public Foo(ITestOutputHelper underlying)
	{
		_underlying=underlying;
	}
	
	public void WriteLine(string message)
	{
		Recent = message;
		_underlying.WriteLine(message);
	}

	public void WriteLine(string format, params object[] args)
	{
		Recent = string.Format(format, args);
		_underlying.WriteLine(format, args);
	}
}

public class TestLoggerTests
{
	private readonly ITestOutputHelper _testOutputHelper;

	public TestLoggerTests(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}

	[Fact]
	public void CtorTest()
	{
		var dummy = A.Dummy<ITestOutputHelper>();

		TestLogger fixtire = new(dummy);
		fixtire.MinimumLogLevel.Is(LogLevel.Information);

		fixtire=new TestLogger(dummy,LogLevel.Debug);
		fixtire.MinimumLogLevel.Is(LogLevel.Debug);
	}

	[Fact]
	public void LogDebugTest()
	{
		var logger=new TestLogger(_testOutputHelper,LogLevel.Debug);
		logger.LogDebug("Hello");
	}
}