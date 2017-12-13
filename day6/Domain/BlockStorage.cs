using System;
using System.Collections.Generic;
using System.Linq;

namespace day6.Domain
{
    public class BlockStorage
    {
        private readonly List<int> _blocks;

        public BlockStorage()
        {
            _blocks = new List<int>();
        }

        public BlockStorage(IEnumerable<int> blocks)
            : this()
        {
            _blocks = blocks.ToList();
        }

        public BlockStorage(string blocks, IEnumerable<string> separators = null)
        {
            if (separators == null || separators.Count() == 0)
            {
                separators = new[] { " " };
            }

            _blocks = blocks.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                {
                    return int.TryParse(x, out int result) ? result : 0;
                })
                .ToList();
        }

        public IList<int> Blocks
        {
            get
            {
                return _blocks;
            }
        }

        public void Increment(int index)
        {
            _blocks[index]++;
        }

        public BlockStorage Redistribute()
        {
            var blockStorage = new BlockStorage(this.Blocks);
            int maxPos = 0;
            int max = 0;
            for (int i = 0; i < blockStorage.Blocks.Count(); i++)
            {
                if (blockStorage.Blocks[i] > max)
                {
                    maxPos = i;
                    max = blockStorage.Blocks[i];
                }
            }

            blockStorage.Blocks[maxPos] = 0;
            int toRedistribute = max;
            int pos = maxPos;
            while (toRedistribute > 0)
            {
                pos++;
                if (pos >= blockStorage.Blocks.Count())
                {
                    pos = 0;
                }

                blockStorage.Increment(pos);
                toRedistribute--;
            }

            return blockStorage;
        }

        public override bool Equals(object obj)
        {
            BlockStorage other = obj as BlockStorage;
            if (obj == null)
            {
                return false;
            }

            if (this.Blocks.Count() != other.Blocks.Count())
            {
                return false;
            }

            for (var i = 0; i < this.Blocks.Count(); i++)
            {
                if (this.Blocks[i] != other.Blocks[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return string.Join(' ', Blocks);
        }
    }
}