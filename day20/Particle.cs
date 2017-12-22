using System;
using System.Linq;

namespace day20
{
    public class Particle
    {
        public Particle(string initialState)
        {
            var components = initialState.Split(", ").Select(x => x.Trim());
            foreach (var c in components)
            {
                var parts = c.Split('=').Select(x => x.Trim()).ToList();
                var coords = parts[1].Replace("<", string.Empty).Replace(">", string.Empty).Split(',').ToList();
                var parsedCoords = (long.Parse(coords[0]), long.Parse(coords[1]), long.Parse(coords[2]));
                switch (parts[0])
                {
                    case "p":
                        this.Position = parsedCoords;
                        break;
                    case "v":
                        this.Velocity = parsedCoords;
                        break;
                    case "a":
                        this.Acceleration = parsedCoords;
                        break;
                }
            }
        }

        public (long X, long Y, long Z) Position { get; private set; }

        public (long X, long Y, long Z) Velocity { get; private set; }

        public (long X, long Y, long Z) Acceleration { get; private set; }

        public long Distance
        {
            get
            {
                return Math.Abs(this.Position.X) + Math.Abs(this.Position.Y) + Math.Abs(this.Position.Z);
            }
        }

        public void Recalculate()
        {
            this.Velocity = 
                (this.Velocity.X + this.Acceleration.X, 
                 this.Velocity.Y + this.Acceleration.Y, 
                 this.Velocity.Z + this.Acceleration.Z);

            this.Position = 
                (this.Position.X + this.Velocity.X, 
                 this.Position.Y + this.Velocity.Y, 
                 this.Position.Z + this.Velocity.Z);
        }

        public override string ToString()
        {
            return $"p=<{this.Position.X},{this.Position.Y},{this.Position.Z}>, v=<{this.Velocity.X},{this.Velocity.Y},{this.Velocity.Z}>, a=<{this.Acceleration.X},{this.Acceleration.Y},{this.Acceleration.Z}>";
        }
    }
}
