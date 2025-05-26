// See https://aka.ms/new-console-template for more information

using System.Text;
using Microsoft.Extensions.Logging;
using XunitTestLogger;

namespace Playground;

static class MainEntry
{
	private static readonly IReadOnlyDictionary<char, string> EscapeSequences = new Dictionary<char, string>
	{
		['\''] = @"\'",
		['\"'] = @"\""",
		['\\'] = @"\\",
		['0'] = @"\0",
		['\a'] = @"\a",
		['\b'] = @"\b",
		['\e'] = @"\e",
		['\f'] = @"\f",
		['\n'] = @"\n",
		['\r'] = @"\r",
		['\t'] = @"\t",
		['\v'] = @"\v"
	};

	static string ConvertToEscape(string s)
	{
		var bld = new StringBuilder();

		foreach (var c in s)
		{
			if (EscapeSequences.TryGetValue(c, out var escape))
			{
				bld.Append(escape);	
			}
			else
			{
				bld.Append(c);
			}
		}
		
		return bld.ToString();
	}

	private static void Main()
	{
		var dummy=new DummyHelper();
		var logger = new TestLogger(dummy);
		
		logger.LogDebug("DEBUG");
		
		var ret=ConvertToEscape(dummy.Recent);
		Console.WriteLine(ret);
	}
}