using System;
using System.IO;

namespace day19
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: dotnet run <puzzleInputFile>");
            }

            var pr = new PacketRoute(File.ReadAllLines(args[0]));
            pr.Travel();
            Console.WriteLine(pr.Word);
            Console.WriteLine(pr.Steps);
        }
    }
}
