using System;

namespace ExpTreesSamples
{
    public static class ConsoleWrapper
    {
        public static void WriteLineInColor(ConsoleColor color, string text)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = foregroundColor;
        }
    }
}
