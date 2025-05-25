// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

Dictionary<ConsoleColor, int> foregroundColors = new ()
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
	[ConsoleColor.White] = 97,
};


Dictionary<ConsoleColor, int> backgroundColors = new()
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
	[ConsoleColor.White] = 107,
};

Console.WriteLine("\x1b[31;47mRed on White\x1b[0m");

foreach (var (fKey,fValue) in foregroundColors)
foreach (var (bKey,bValue) in backgroundColors)
{
	Console.WriteLine($"\x1b[{fValue};{bValue}mForeground:{fKey} Background:{bKey}\x1b[40;37m");
}

Console.ReadLine();