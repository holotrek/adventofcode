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
            this.Word = string.Empty;
            this.Route = data.ToList();
            _curIndex = (this.Route[0].IndexOf('|'), 0);
            _curDir = Direction.South;
        }

        public string Word { get; private set; }    

        public List<string> Route { get; private set; }

        public void Travel()
        {
            while (this.Move()) { }
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

            Console.WriteLine($"Next character: {chr}");
            if (Char.IsLetter(chr))
            {
                this.Word += chr;
            }
            else if (chr == '+')
            {
                if (_curDir == Direction.North || _curDir == Direction.South)
                {
                    if (this.Route[_curIndex.Y][_curIndex.X + 1] != ' ')
                    {
                        _curDir = Direction.East;
                    }
                    else if (this.Route[_curIndex.Y][_curIndex.X - 1] != ' ')
                    {
                        _curDir = Direction.West;
                    }
                }
                else
                {
                    if (this.Route[_curIndex.Y + 1][_curIndex.X] != ' ')
                    {
                        _curDir = Direction.South;
                    }
                    else if (this.Route[_curIndex.Y - 1][_curIndex.X] != ' ')
                    {
                        _curDir = Direction.North;
                    }
                }
            }

            return this.GetNextCharacter() != ' ';
        }
    }
}