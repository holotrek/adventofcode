using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using day6.Domain;

namespace day6
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

            var data = File.ReadAllText(args[1]);
            var block = new BlockStorage(data, new [] { " ", "\t" });
            Console.WriteLine(block.ToString());

            var previousBlocks = new List<BlockStorage>();
            var cycles = 0;
            while (!previousBlocks.Where(x => x.Equals(block)).Any())
            {
                previousBlocks.Add(block);
                block = block.Redistribute();
                Console.WriteLine(block.ToString());
                cycles++;
            }

            switch (args[0])
            {
                case "part1":
                    Console.WriteLine($"Redistribution matched after {cycles} cycles.");
                    break;
                case "part2":
                    var matchingBlockIndex = -1;
                    var i = previousBlocks.Count() - 1;
                    while (matchingBlockIndex == -1)
                    {
                        if (previousBlocks[i].Equals(block))
                        {
                            matchingBlockIndex = i;
                        }
                        i--;
                    }

                    Console.WriteLine($"Count between matching distributions: {previousBlocks.Count() - matchingBlockIndex}.");
                    break;
            }
        }
    }
}
