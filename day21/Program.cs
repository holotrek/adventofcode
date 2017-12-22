using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day21
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run <part1|part2> <puzzleInputFile> [iterations (def: 5)]");
            }

            int iter = 5;
            if (args.Length > 2)
            {
                int.TryParse(args[2], out iter);
            }

            var pg = new PatternGrid();
            pg.SetEnhancements(File.ReadAllLines(args[1]));
            if (args[0] == "part1")
            {
            //     var lists = new List<List<char>>();
            //     lists.Add(new [] { '#', '#', '.', '#', '.', '#', '.', '.', '#' }.ToList());
            //     lists.Add(new [] { '#', '#', '.', '#', '.', '#', '.', '.', '#' }.ToList());
            //     lists.Add(new [] { '#', '.', '#', '#', '.', '#', '#', '#', '#' }.ToList());
            //     lists.Add(new [] { '.', '#', '.', '#', '.', '#', '#', '#', '.' }.ToList());
            //     lists.Add(new [] { '#', '.', '#', '#', '.', '#', '#', '#', '#' }.ToList());
            //     lists.Add(new [] { '#', '.', '#', '#', '.', '#', '.', '.', '.' }.ToList());
            //     lists.Add(new [] { '#', '#', '.', '#', '.', '#', '.', '.', '#' }.ToList());
            //     lists.Add(new [] { '#', '.', '#', '#', '.', '#', '#', '#', '#' }.ToList());
            //     lists.Add(new [] { '#', '.', '#', '#', '.', '#', '#', '#', '#' }.ToList());
            //     var combined = pg.Combine(lists);
            //     Console.WriteLine(string.Join(' ', combined));

                // var lists = new List<List<char>>();
                // lists.Add(new [] { '0', '1', '2', '3', '4', '5', '6', '7', '8' }.ToList());
                // lists.Add(new [] { '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }.ToList());
                // lists.Add(new [] { 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q' }.ToList());
                // lists.Add(new [] { 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }.ToList());
                // lists.Add(new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' }.ToList());
                // lists.Add(new [] { 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' }.ToList());
                // lists.Add(new [] { 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!' }.ToList());
                // lists.Add(new [] { '@', '#', '$', '%', '^', '&', '*', '(', ')' }.ToList());
                // lists.Add(new [] { '-', '_', '+', '=', '[', ']', '{', '}', '|' }.ToList());
                // var combined = pg.Combine(lists);
                // Console.WriteLine(string.Join(' ', combined));


                Console.WriteLine(pg);
                for (int i = 0; i < iter; i++)
                {
                    Console.WriteLine(pg.DoEnhancement());
                }
                Console.WriteLine($"{pg.Grid.Where(x => x).Count()} pixels on.");
            }
            else
            {
            }
        }
    }
}
