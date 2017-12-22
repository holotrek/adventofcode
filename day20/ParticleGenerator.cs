using System;
using System.Collections.Generic;
using System.Linq;

namespace day20
{
    public class ParticleGenerator
    {
        private readonly List<Particle> _particles;

        public ParticleGenerator(IEnumerable<string> data)
        {
            _particles = data.Select(x => new Particle(x)).ToList();
        }

        public int ClosestParticle { get; private set; } = -1;

        public long MinDistance { get; private set; } = int.MaxValue;

        public void Recalculate()
        {
            this.ClosestParticle = -1;
            this.MinDistance = int.MaxValue;
            for (var i = 0; i < _particles.Count(); i++)
            {
                _particles[i].Recalculate();
                if (_particles[i].Distance < MinDistance)
                {
                    this.MinDistance = _particles[i].Distance;
                    this.ClosestParticle = i;
                }
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _particles.Select(x => x.ToString()));
        }
    }
}