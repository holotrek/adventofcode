using System;
using System.Collections.Generic;
using System.Linq;

namespace day21
{
    public class PatternGrid
    {
        private readonly List<bool> _grid = new[] { false, true, false, false, false, true, true, true, true }.ToList();

        private int _size = 3;

        private readonly List<string> _enhancements2d = new List<string>();

        private readonly List<string> _enhancements3d = new List<string>();

        public PatternGrid()
        {
        }

        public IEnumerable<bool> Grid
        {
            get
            {
                return _grid;
            }
        }

        public void SetEnhancements(IEnumerable<string> enhancements)
        {
            foreach (var e in enhancements)
            {
                var parts = e.Split("=>").Select(x => x.Trim()).ToList();
                if (parts[0].Split('/').Count() == 2)
                {
                    _enhancements2d.Add(e);
                }
                else
                {
                    _enhancements3d.Add(e);
                }
            }
        }

        public string DoEnhancement()
        {
            int mod = _size % 2 == 0 ? 2 : 3;
            int squareCount = (int)Math.Pow(_size / mod, 2);

            IEnumerable<IEnumerable<bool>> sections = this.Segment(mod, squareCount, _grid);

            var newGrid = new List<List<bool>>();
            foreach (var s in sections)
            {
                Console.Write($"Block {string.Join(' ', s.Select(x => x ? '#' : '.'))} enhanced using... ");
                var e = this.FindMatchingEnhancement(mod, s);
                if (e == null)
                {
                    throw new Exception("Found no enhancement!!");
                }

                newGrid.Add(e.ToList());
            }

            _grid.Clear();
            _grid.AddRange(this.Combine(newGrid));
            _size = (int)Math.Sqrt(_grid.Count());
            return this.ToString();
        }

        public override string ToString()
        {
            var lines = this.Split(_grid, (int)Math.Sqrt(_grid.Count()));
            var result = string.Empty;
            foreach (var line in lines)
            {
                result += string.Join(' ', line.Select(x => x ? '#' : '.')) + Environment.NewLine;
            }
            result += Environment.NewLine;
            return result;
        }

        private IEnumerable<bool> FindMatchingEnhancement(int mod, IEnumerable<bool> gridSection)
        {
            List<string> enList;
            if (mod == 2)
            {
                enList = _enhancements2d;
            }
            else
            {
                enList = _enhancements3d;
            }

            foreach (string e in enList)
            {
                var keyVal = e.Split("=>").Select(x => x.Trim()).ToList();
                IEnumerable<bool> testGrid = this.StringToList(keyVal[0]);
                bool matched = false;
                int rotateCount = 0;
                while (!matched && rotateCount < 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        testGrid = this.Flip(mod, testGrid);

                        if (testGrid.SequenceEqual(gridSection))
                        {
                            matched = true;
                            break;
                        }
                    }

                    testGrid = this.Rotate(mod, testGrid);
                    rotateCount++;
                }

                if (matched)
                {
                    Console.WriteLine(keyVal[1]);
                    return this.StringToList(keyVal[1]);
                }
            }

            return null;
        }

        private IEnumerable<bool> Flip(int mod, IEnumerable<bool> gridSection)
        {
            var flipped = new List<bool>();
            foreach (var line in this.Split(gridSection.ToList(), mod))
            {
                flipped.AddRange(line.Reverse());
            }
            return flipped;
        }

        private IEnumerable<bool> Rotate(int mod, IEnumerable<bool> gridSection)
        {
            var rotated = new List<bool>();
            for (var add = 0; add < mod; add++)
            {
                for (var mult = mod - 1; mult >= 0; mult--)
                {
                    rotated.Add(gridSection.ToList()[mod * mult + add]);
                }
            }
            return rotated;
        }

        public IEnumerable<T> Combine<T>(List<List<T>> lists)
        {
            var combined = new List<T>();
            int squareCount = lists.Count();
            int listCount = lists[0].Count();
            int mod = (int)Math.Sqrt(listCount);
            int listIdx = 0;
            int x = 0;
            int y = 0;

            if (squareCount == 1)
            {
                return lists[0];
            }

            for (int i = 0; i < squareCount * listCount; i++)
            {
                //Console.WriteLine($"y={y},listIdx={listIdx},x={x}");
                combined.Add(lists[listIdx][y * mod + x]);
                if ((i + 1) % mod == 0)
                {
                    if ((listIdx + 1) % (int)Math.Sqrt(squareCount) == 0)
                    {
                        if ((y + 1) % mod == 0)
                        {
                            listIdx++;
                            y = 0;
                        }
                        else
                        {
                            y++;
                            listIdx = listIdx - (int)Math.Sqrt(squareCount) + 1;
                        }
                    }
                    else
                    {
                        listIdx++;
                    }

                    x = 0;
                }
                else
                {
                    x++;
                }
            }

            return combined;
        }

        private IEnumerable<IEnumerable<T>> Segment<T>(int mod, int squareCount, List<T> list)
        {
            var segmented = new List<List<T>>();
            int blkIdx = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                //Console.WriteLine($"i={i},blkIdx={blkIdx},grid[i]={_grid[i]}");
                List<T> curBlk;
                if (segmented.Count() <= blkIdx)
                {
                    curBlk = new List<T>();
                    segmented.Add(curBlk);
                }
                else
                {
                    curBlk = segmented[blkIdx];
                }

                curBlk.Add(list[i]);

                if ((i + 1) % mod == 0)
                {
                    if ((blkIdx + 1) % (int)Math.Sqrt(squareCount) == 0)
                    {
                        if (curBlk.Count() == (_grid.Count() / squareCount))
                        {
                            blkIdx++;
                        }
                        else
                        {
                            blkIdx = blkIdx - (int)Math.Sqrt(squareCount) + 1;
                        }
                    }
                    else
                    {
                        blkIdx++;
                    }
                }
            }

            return segmented;
        }

        private IEnumerable<IEnumerable<bool>> Split(List<bool> list, int pieces)
        {
            var result = new List<IEnumerable<bool>>();
            var curList = new List<bool>();
            var listSize = list.Count() / pieces;
            for (int i = 0; i < pieces; i++)
            {
                curList = new List<bool>();
                for (int j = 0; j < listSize; j++)
                {
                    curList.Add(list[i*listSize + j]);
                }
                result.Add(curList);
            }
            return result;
        }

        private IEnumerable<bool> StringToList(string gridString)
        {
            return gridString.Replace("/", string.Empty).Select(x => x == '#');
        }
    }
}