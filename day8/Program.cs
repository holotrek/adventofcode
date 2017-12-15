using System;
using System.IO;
using day8.Domain;

namespace day8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run <part1|part2> <puzzleInputFile>");
                return;
            }
            
            var data = File.ReadAllLines(args[1]);
            var rr = new RegisterRunner(data);
            rr.Run();
            switch (args[0])
            {
                case "part1":
                    Console.WriteLine(rr.Max());
                    break;
                case "part2":
                    Console.WriteLine(rr.HighestValueEver);
                    break;
            }
        }
    }
}
