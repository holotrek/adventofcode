using System;
using System.IO;

namespace day20
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run <part1|part2> <puzzleInputFile> [iterations (def: 1000)]");
            }

            int iter = 1000;
            if (args.Length > 2)
            {
                int.TryParse(args[2], out iter);
            }

            var pg = new ParticleGenerator(File.ReadAllLines(args[1]));

            if (args[0] == "part1") 
            {
                for (int i = 0; i < iter; i++)
                {
                    pg.Recalculate(false);
                    Console.WriteLine(pg.ClosestParticle);
                }
            }
            else
            {
                for (int i = 0; i < iter; i++)
                {
                    pg.Recalculate(true);
                    Console.WriteLine(pg.Count());
                }
            }
        }
    }
}
