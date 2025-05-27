using Xunit.Abstractions;

namespace Playground;

public class DummyTestOutput : ITestOutputHelper
{
	public void WriteLine(string message)
	{
		WriteLine(message, []);
	}

	public void WriteLine(string format, params object[] args)
	{
		Console.WriteLine(format, args);
	}
}