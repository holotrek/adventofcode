
namespace day8.Domain
{
    public class Register
    {
        public Register(string name)
        {
            this.Name = name;
            this.Value = 0;
        }

        public string Name { get; private set; }

        public int Value { get; private set; }

        public void Increment(int amount)
        {
            this.Value += amount;
        }
    }
}