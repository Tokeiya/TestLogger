using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using XunitTestLogger;

namespace XunitTestLoggerTests;

file interface ISample
{
}

public class TypedTestLoggerTests
{
	[Fact]
	public void CtorTest()
	{
		var dummy = new Mock<ITestOutputHelper>();

		TestLogger<ISample> fixtire = new(dummy.Object);
		fixtire.MinimumLogLevel.Is(LogLevel.Information);

		fixtire = new(dummy.Object, LogLevel.Debug);
		fixtire.MinimumLogLevel.Is(LogLevel.Debug);
	}


	[Fact]
	public void LogTraceTest()
	{

		var helper = new Mock<ITestOutputHelper>();
		var logger = new TestLogger(helper.Object, LogLevel.Trace);
		logger.LogTrace("trace message");
		helper.Verify(h => h.WriteLine("\e[94;49mTrace\e[39;49m\n0\ntrace message\n\n"), Times.Once);
	}

	[Fact]
	public void PassLogTraceTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var logger = new TestLogger<ISample>(helper.Object);
		logger.LogTrace("trace message");
		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);
	}


	[Fact]
	public void LogDebugTest()
	{

		var helper = new Mock<ITestOutputHelper>();
		var logger = new TestLogger<ISample>(helper.Object, LogLevel.Trace);

		logger.LogDebug("\"hello world\"");
		helper.Verify(h => h.WriteLine("\e[96;49mDebug\e[39;49m\n0\n\"hello world\"\n\n"), Times.Once);
	}


	[Fact]
	public void PassLogDebugTest()
	{
		var helper = new Mock<ITestOutputHelper>();

		var dispatchTo = new Mock<ILogger<String>>();
		var logger = new TestLogger<String>(helper.Object, LogLevel.Information, dispatchTo.Object);

		logger.LogDebug("\"hello world\"");

		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

		dispatchTo.Verify(m => m.Log(
			LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
	}

	[Fact]
	public void LogInformationTest()
	{
		var helper = new Mock<ITestOutputHelper>();

		var dispatchTo = new Mock<ILogger<String>>();
		var logger = new TestLogger<String>(helper.Object, LogLevel.Information, dispatchTo.Object);

		logger.LogInformation("information");

		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once);



		dispatchTo.Verify(m => m.Log(
			LogLevel.Information, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
	}

	[Fact]
	public void PassLogInformationTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();
		var logger = new TestLogger<String>(helper.Object, LogLevel.Warning, dispatchTo.Object);

		logger.LogInformation("information");
		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);
		dispatchTo.Verify(m => m.Log(
			LogLevel.Information, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
	}

	[Fact]
	public void LogWarningTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();

		var logger = new TestLogger<String>(helper.Object, LogLevel.Warning, dispatchTo.Object);
		logger.LogWarning("Warning");
		helper.Verify(h => h.WriteLine("\e[97;43mWarning\e[39;49m\n0\nWarning\n\n"), Times.Once);

		dispatchTo.Verify(m => m.Log(
			LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
	}


	[Fact]
	public void PassWarningTetst()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();

		var logger = new TestLogger<String>(helper.Object, LogLevel.Error, dispatchTo.Object);
		logger.LogWarning("Warning");
		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

		dispatchTo.Verify(m => m.Log(
			LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);

	}


	[Fact]
	public void LogErrorTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();
		
		var logger = new TestLogger(helper.Object, LogLevel.Error, dispatchTo.Object);
		logger.LogError("Error");
		helper.Verify(h => h.WriteLine("\e[97;45mError\e[39;49m\n0\nError\n\n"), Times.Once);
		
		dispatchTo.Verify(m => m.Log(
			LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
		
	}

	[Fact]
	public void PassErrorTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();

		var logger = new TestLogger<String>(helper.Object, LogLevel.Critical, dispatchTo.Object);
		logger.LogError("Error");
		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

		dispatchTo.Verify(m => m.Log(
			LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
	}

	[Fact]
	public void LogCriticalTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();
		var logger = new TestLogger<String>(helper.Object, LogLevel.Critical, dispatchTo.Object);
		logger.LogCritical("Critical");
		helper.Verify(h => h.WriteLine("\e[97;41mCritical\e[39;49m\n0\nCritical\n\n"), Times.Once);

		dispatchTo.Verify(m => m.Log(
			LogLevel.Critical, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);

	}

	[Fact]
	public void PassCriticalTest()
	{
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();

		var logger = new TestLogger<String>(helper.Object, LogLevel.None, dispatchTo.Object);
		logger.LogCritical("Critical");
		helper.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

		dispatchTo.Verify(m => m.Log(
			LogLevel.Critical, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null,
			It.IsAny<Func<It.IsAnyType, Exception?, string>>()
		), Times.Once);
		//
	}

	[Fact]
	public void LogExceptionTest()
	{
		//\e[97;45mError\e[39;49m\n0\nLogMessage\n\e[97;101mArgumentException\e[39;49m\nException Message\n\n
		var helper = new Mock<ITestOutputHelper>();
		var dispatchTo = new Mock<ILogger<String>>();
		var logger = new TestLogger<String>(helper.Object, dispatchTo: dispatchTo.Object);
		var ex = new ArgumentException("Exception Message");
		logger.LogError(ex, "LogMessage");

		helper.Verify(m =>
				m.WriteLine(
					"\e[97;45mError\e[39;49m\n0\nLogMessage\n\e[97;101mArgumentException\e[39;49m\nException Message\n\n")
			, Times.Once);
		;
	}
}