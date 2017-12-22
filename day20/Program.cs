using System;
using System.IO;

namespace day20
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: dotnet run <puzzleInputFile> [iterations (def: 1000)]");
            }

            int iter = 1000;
            if (args.Length > 1)
            {
                int.TryParse(args[1], out iter);
            }

            var pg = new ParticleGenerator(File.ReadAllLines(args[0]));
            File.WriteAllText("test.txt", pg.ToString());
            for (int i = 0; i < iter; i++)
            {
                pg.Recalculate();
                Console.WriteLine(pg.ClosestParticle);
            }
        }
    }
}
