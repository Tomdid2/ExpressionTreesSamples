using ExpTreesSamples.Samples;
using System;
using System.Collections.Generic;

namespace ExpTreesSamples
{
    public class Program
    {
        public static bool KeepRunning = true;

        public static void Main(string[] args)
        {
            while (KeepRunning)
            {
                var samples = GetSamples();

                ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Dostępne sample:\n\r");

                foreach (var sample in samples)
                {
                    ConsoleWrapper.WriteLineInColor(ConsoleColor.White, sample.Key + ": " + sample.Value.Description);
                }

                var key = Console.ReadLine();

                if (key == "cls")
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine();

                    if (samples.TryGetValue(key, out ISample foundSample))
                    {
                        foundSample.Run();
                    }
                    else
                    {
                        ConsoleWrapper.WriteLineInColor(ConsoleColor.Red, "Nieprawidłowy indeks!");
                    }

                    ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "\n\r==========================");
                }
            }
        }

        private static Dictionary<string, ISample> GetSamples()
        {
            return new Dictionary<string, ISample>
            {
                { "1", new BasicVisualizationSample() },
                { "2", new DynamicWhereSample() },
                { "3", new AddMethodCallOnProperty() },
                { "4", new MergeTwoExpressions() }
            };
        }
    }
}
