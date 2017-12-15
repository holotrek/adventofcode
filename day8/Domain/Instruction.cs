
using System.Collections.Generic;
using System.Linq;

namespace day8.Domain
{
    public class Instruction
    {
        private readonly string _registerName;
        private readonly bool _increase;
        private readonly int _value;
        private readonly string _conditionRegister;
        private readonly string _condition;
        private readonly int _conditionValue;

        public Instruction(string registerName, bool increase, int value, string conditionRegister, string condition, int conditionValue)
        {
            _registerName = registerName;
            _increase = increase;
            _value = value;
            _conditionRegister = conditionRegister;
            _condition = condition;
            _conditionValue = conditionValue;
        }

        public bool Execute(List<Register> registers)
        {
            var register = registers.Where(x => x.Name == _registerName).FirstOrDefault();
            var condRegister = registers.Where(x => x.Name == _conditionRegister).FirstOrDefault();
            if (register != null && condRegister != null)
            {
                bool conditionMatched = false;
                switch (_condition)
                {
                    case "==":
                        conditionMatched = condRegister.Value == _conditionValue;
                        break;
                    case "!=":
                        conditionMatched = condRegister.Value != _conditionValue;
                        break;
                    case "<":
                        conditionMatched = condRegister.Value < _conditionValue;
                        break;
                    case "<=":
                        conditionMatched = condRegister.Value <= _conditionValue;
                        break;
                    case ">":
                        conditionMatched = condRegister.Value > _conditionValue;
                        break;
                    case ">=":
                        conditionMatched = condRegister.Value >= _conditionValue;
                        break;
                }

                if (conditionMatched)
                {
                    register.Increment((_increase ? 1 : -1) * _value);
                }

                return conditionMatched;
            }
            
            return false;
        }

        public override string ToString()
        {
            string incText = _increase ? "inc" : "dec";
            return $"{_registerName} {incText} {_value} if {_conditionRegister} {_condition} {_conditionValue}";
        }

        public enum InstructionCondition
        {
            Equals,
            NotEquals,
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreaterThanOrEqual
        }
    }
}