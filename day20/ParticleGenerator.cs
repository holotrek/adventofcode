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
            int i = -1;
            _particles = data.Select(x => {
                i++;
                return new Particle(i, x);
            }).ToList();
        }

        public int ClosestParticle { get; private set; } = -1;

        public long MinDistance { get; private set; } = int.MaxValue;

        public void Recalculate(bool removeCollisions)
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

            if (removeCollisions)
            {
                foreach (var grp in _particles.GroupBy(x => x.Position))
                {
                    if (grp.Count() > 1)
                    {
                        _particles.RemoveAll(x => grp.Where(y => y.Id == x.Id).Any());
                    }
                }
            }
        }

        public int Count()
        {
            return _particles.Count();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _particles.Select(x => x.ToString()));
        }
    }
}