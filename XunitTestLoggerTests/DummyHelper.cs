using System.Text;
using Xunit.Abstractions;

namespace XunitTestLoggerTests;

public class DummyHelper : ITestOutputHelper
{
	static private readonly IReadOnlyDictionary<char, string> EscapeSequences = new Dictionary<char, string>
	{
		['\''] = @"\'",
		['\"'] = @"\""",
		['\\'] = @"\",
		['\0'] = @"\0",
		['\a'] = @"\a",
		['\b'] = @"\b",
		['\e'] = @"\e",
		['\f'] = @"\f",
		['\n'] = @"\n",
		['\r'] = @"\r",
		['\t'] = @"\t",
		['\v'] = @"\v",
	};
	
	private readonly ITestOutputHelper _helper;

	public DummyHelper(ITestOutputHelper helper)
	{
		_helper = helper;
	}

	
	public string Recent { get; private set; } = "";
	
	public bool DoOutput { get; set; } = true;

	public string EscapedString
	{
		get
		{
			var bld = new StringBuilder();
			foreach (var c in Recent)
			{
				if (EscapeSequences.TryGetValue(c, out var escaped))
				{
					bld.Append(escaped);
				}
				else
				{
					bld.Append(c);
				}
			}
			
			return bld.ToString();
		}
	}

	public void WriteLine(string message)
	{
		WriteLine(message, []);
	}

	public void WriteLine(string format, params object[] args)
	{
		Recent = string.Format(format, args);
		
		if(DoOutput) _helper.WriteLine(Recent);
	}
}