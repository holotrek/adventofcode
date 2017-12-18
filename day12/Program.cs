using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error: Puzzle input file required.");
                return;
            }

            var pn = new PipeNetwork(File.ReadAllLines(args[0]));
            var group = new List<Pipe>();
            pn.PopulateGroup(ref group);
            int zeroGroupTotal = group.Select(x => x.Number).Distinct().Count();
            int groupCount = pn.GetNumberOfGroups();
            Console.WriteLine($"Group 0 contains {zeroGroupTotal} numbers.");
            Console.WriteLine($"There are {groupCount} total individual groups.");
        }
    }
}
