// See https://aka.ms/new-console-template for more information

using System.Text;

static class MainEntry
{
	private static readonly ConsoleColor DefaultForegroundColor = Console.ForegroundColor;
	private static readonly ConsoleColor DefaultBackgroundColor = Console.BackgroundColor;

	private static readonly IReadOnlyDictionary<ConsoleColor, int> ForegroundColors = new Dictionary<ConsoleColor, int>
	{
		[ConsoleColor.Black] = 30,
		[ConsoleColor.DarkRed] = 31,
		[ConsoleColor.DarkGreen] = 32,
		[ConsoleColor.DarkYellow] = 33,
		[ConsoleColor.DarkBlue] = 34,
		[ConsoleColor.DarkMagenta] = 35,
		[ConsoleColor.DarkCyan] = 36,
		[ConsoleColor.DarkGray] = 90,
		[ConsoleColor.Gray] = 37,
		[ConsoleColor.Red] = 91,
		[ConsoleColor.Green] = 92,
		[ConsoleColor.Yellow] = 93,
		[ConsoleColor.Blue] = 94,
		[ConsoleColor.Magenta] = 95,
		[ConsoleColor.Cyan] = 96,
		[ConsoleColor.White] = 97
	};


	private static readonly IReadOnlyDictionary<ConsoleColor, int> BackgroundColors = new Dictionary<ConsoleColor, int>
	{
		[ConsoleColor.Black] = 40,
		[ConsoleColor.DarkRed] = 41,
		[ConsoleColor.DarkGreen] = 42,
		[ConsoleColor.DarkYellow] = 43,
		[ConsoleColor.DarkBlue] = 44,
		[ConsoleColor.DarkMagenta] = 45,
		[ConsoleColor.DarkCyan] = 46,
		[ConsoleColor.DarkGray] = 100,
		[ConsoleColor.Gray] = 47,
		[ConsoleColor.Red] = 101,
		[ConsoleColor.Green] = 102,
		[ConsoleColor.Yellow] = 103,
		[ConsoleColor.Blue] = 104,
		[ConsoleColor.Magenta] = 105,
		[ConsoleColor.Cyan] = 106,
		[ConsoleColor.White] = 107
	};

	private static void SetColor(StringBuilder builder, ConsoleColor foreground, ConsoleColor background)
	{
		builder.Append($"\x1b[{ForegroundColors[foreground]};{BackgroundColors[background]}m");
	}

	private static void SetColor(StringBuilder builder, (int foreground, int background) tuple)
	{
		builder.Append($"\x1b[{tuple.foreground};{tuple.background}m");
	}

	private static void ResetColor(StringBuilder builder)
	{
		builder.Append($"\x1b[{ForegroundColors[DefaultForegroundColor]};{BackgroundColors[DefaultBackgroundColor]}m");
	}

	private static void Main()
	{
		var bld = new StringBuilder();
		SetColor(bld, ConsoleColor.Red, ConsoleColor.Green);
		bld.Append("Hello");
		bld.Append("World");
		ResetColor(bld);
		Console.WriteLine(bld);
		Console.WriteLine($"Fore:{Console.ForegroundColor},Back:{Console.BackgroundColor}");

		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.Write("\x1b[39;49m");

		Console.WriteLine("Reset");


	}
}