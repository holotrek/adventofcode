using System;
using System.Collections.Generic;
using System.Linq;

namespace day12
{
    public class PipeNetwork
    {
        private readonly List<Pipe> _pipes;

        public PipeNetwork(string[] lines)
        {
            _pipes = new List<Pipe>();
            foreach (var line in lines)
            {
                string[] parts = line.Split(new [] { "<->" }, StringSplitOptions.RemoveEmptyEntries);
                int num = int.Parse(parts[0].Trim());
                List<int> assocNums = new List<int>();
                if (parts.Length > 1)
                {
                    string[] associated = parts[1].Trim().Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string a in associated)
                    {
                        assocNums.Add(int.Parse(a.Trim()));
                    }
                }
                _pipes.Add(new Pipe(num, assocNums));
            }

            foreach (var p in _pipes)
            {
                p.AssociatePipes(_pipes);
            }
        }

        public void PopulateGroup(ref List<Pipe> group, int number = 0)
        {
            var pipe = _pipes.Where(x => x.Number == number).FirstOrDefault();
            if (pipe != null)
            {
                foreach (Pipe p in pipe.AssociatedPipes)
                {
                    if (!group.Where(x => x.Number == p.Number).Any())
                    {
                        group.Add(p);
                        this.PopulateGroup(ref group, p.Number);
                    }
                }
            }
        }

        public int GetNumberOfGroups()
        {
            int num = 0;
            var numbersInGroups = new List<int>();
            foreach (var p in _pipes)
            {
                if (!numbersInGroups.Contains(p.Number))
                {
                    var plist = new List<Pipe>();
                    this.PopulateGroup(ref plist, p.Number);
                    numbersInGroups.AddRange(plist.Select(x => x.Number).Distinct());
                    num++;
                }
            }
            
            return num;
        }
    }

    public class Pipe
    {
        private readonly List<int> _pipeNumbers;
        private readonly List<Pipe> _associatedPipes;

        public Pipe(int num, IEnumerable<int> pipeNumbers)
        {
            this.Number = num;
            _pipeNumbers = pipeNumbers.ToList();
            _associatedPipes = new List<Pipe>();
        }

        public int Number { get; private set; }

        public IList<Pipe> AssociatedPipes
        {
            get
            {
                return _associatedPipes;
            }
        }

        public void AssociatePipes(List<Pipe> allPipes)
        {
            foreach (int p in _pipeNumbers)
            {
                var pipe = allPipes.Where(x => x.Number == p).FirstOrDefault();
                if (pipe != null)
                {
                    if (!_associatedPipes.Contains(pipe))
                    {
                        _associatedPipes.Add(pipe);
                    }
                }
            }
        }
    }
}