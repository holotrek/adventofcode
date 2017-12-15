
using System;
using System.Collections.Generic;
using System.Linq;

namespace day8.Domain
{
    public class RegisterRunner
    {
        private readonly List<Instruction> _instructions;
        private readonly List<Register> _registers;
        public RegisterRunner(string[] lines)
        {
            _instructions = new List<Instruction>();
            _registers = new List<Register>();
            foreach (var l in lines)
            {
                var parts = l.Split(" ");
                var instr = new Instruction(parts[0], parts[1] == "inc", int.Parse(parts[2]), parts[4], parts[5], int.Parse(parts[6]));
                if (!_registers.Where(x => x.Name == parts[0]).Any())
                {
                    _registers.Add(new Register(parts[0]));
                }
                _instructions.Add(instr);
            }
        }

        public int HighestValueEver { get; private set; }

        public void Run()
        {
            foreach (var instr in _instructions)
            {
                Console.Write($"Running instruction {instr}");
                bool matched = instr.Execute(_registers);
                Console.WriteLine(matched ? " (PASSED)" : " (FAILED)");
                this.HighestValueEver = Math.Max(this.HighestValueEver, this.Max());
            }
        }

        public int Max()
        {
            return _registers.Select(x => x.Value).Max();
        }
    }
}