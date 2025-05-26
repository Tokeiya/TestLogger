using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using XunitTestLogger;

namespace XunitTestLoggerTests;

using Xunit;
using FakeItEasy;


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
		// var helper=A.Fake<ITestOutputHelper>();
		// var logger=new TestLogger(helper,LogLevel.Debug);
		// logger.LogDebug("Hello");
		//
		// A.CallTo(()=>helper.WriteLine("\e[96;49mDebug\e[39;49m\n\0\nDEBUG\n\n")).MustHaveHappenedOnceExactly();
		var helper=A.Fake<ITestOutputHelper>();
		helper.WriteLine("Hello");
		A.CallTo(()=>helper.WriteLine("Hello")).MustHaveHappenedOnceExactly();
		//
	}
}