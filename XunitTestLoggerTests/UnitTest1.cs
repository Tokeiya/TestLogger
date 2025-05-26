using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using XunitTestLogger;

namespace XunitTestLoggerTests;

using Xunit;
using FakeItEasy;


public class UnitTest1
{
	private readonly ITestOutputHelper _testOutputHelper;

	public UnitTest1(ITestOutputHelper testOutputHelper)
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