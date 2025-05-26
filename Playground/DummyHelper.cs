using Xunit.Abstractions;

namespace Playground;

public class DummyHelper : ITestOutputHelper
{
	public string Recent { get; private set; } = "";

	public void WriteLine(string message)
	{
		WriteLine(message, []);
	}

	public void WriteLine(string format, params object[] args)
	{
		Recent = string.Format(format, args);
		Console.WriteLine(Recent);
	}
}