using System;
using System.Collections.Generic;
using System.Linq;

namespace day19
{
    public class PacketRoute
    {
        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        private (int X, int Y) _curIndex;
        private Direction _curDir;

        public PacketRoute(IEnumerable<string> data)
        {
            this.Route = data.Where(x => x.Length > 0).ToList();
            _curIndex = (this.Route[0].IndexOf('|'), 0);
            _curDir = Direction.South;
        }

        public string Word { get; private set; } = string.Empty;

        public int Steps { get; private set; } = 1;

        public List<string> Route { get; private set; }

        public void Travel()
        {
            do
            {
                this.Steps++;
            }
            while (this.Move());
        }

        public (int X, int Y) GetNextPosition()
        {
            (int X, int Y) index = (0, 0);
            switch (_curDir)
            {
                case Direction.North:
                    index = (_curIndex.X, _curIndex.Y - 1);
                    break;
                case Direction.East:
                    index = (_curIndex.X + 1, _curIndex.Y);
                    break;
                case Direction.South:
                    index = (_curIndex.X, _curIndex.Y + 1);
                    break;
                case Direction.West:
                    index = (_curIndex.X - 1, _curIndex.Y);
                    break;
            }

            return index;
        }

        public char GetNextCharacter()
        {
            var index = this.GetNextPosition();
            return this.Route[index.Y][index.X];
        }

        public bool Move()
        {
            var chr = this.GetNextCharacter();
            _curIndex = this.GetNextPosition();

            if (Char.IsLetter(chr))
            {
                this.Word += chr;
            }
            else if (chr == '+')
            {
                if (_curDir == Direction.North || _curDir == Direction.South)
                {
                    if (this.Route[_curIndex.Y].Count() > _curIndex.X + 1 && this.Route[_curIndex.Y][_curIndex.X + 1] != ' ')
                    {
                        _curDir = Direction.East;
                    }
                    else if (_curIndex.X > 0 && this.Route[_curIndex.Y][_curIndex.X - 1] != ' ')
                    {
                        _curDir = Direction.West;
                    }
                }
                else
                {
                    if (this.Route.Count() > _curIndex.Y + 1 && this.Route[_curIndex.Y + 1][_curIndex.X] != ' ')
                    {
                        _curDir = Direction.South;
                    }
                    else if (_curIndex.Y > 0 && this.Route[_curIndex.Y - 1][_curIndex.X] != ' ')
                    {
                        _curDir = Direction.North;
                    }
                }
            }

            return this.GetNextCharacter() != ' ';
        }
    }
}