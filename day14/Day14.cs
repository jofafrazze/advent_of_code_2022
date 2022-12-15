using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day14
    {
        // Regolith Reservoir: Let sand fall down and come to rest
        static bool AddSand(Dictionary<Pos, char> map, int maxRow)
        {
            Pos p = new Pos(500, 0);
            bool added = false;
            while (!added && p.y < maxRow && !map.ContainsKey(new Pos(500, 0)))
            {
                if (!map.ContainsKey(p + CoordsRC.down))
                    p += CoordsRC.down;
                else if (!map.ContainsKey(p + CoordsRC.downLeft))
                    p += CoordsRC.downLeft;
                else if (!map.ContainsKey(p + CoordsRC.downRight))
                    p += CoordsRC.downRight;
                else
                {
                    added = true;
                    map[p] = 'o';
                }
            }
            return added;
        }
        static Dictionary<Pos, char> ReadMap(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var map = new Dictionary<Pos, char>();
            foreach (var s in input)
            {
                var ss = s.Replace('-', ' ');
                var nums = Extract.Ints(ss);
                Pos p0, p1 = new Pos();
                for (int i = 0; i < nums.Length; i += 2)
                {
                    p0 = p1;
                    p1 = new Pos(nums[i], nums[i + 1]);
                    if (i > 0)
                    {
                        if (p0.y == p1.y)
                        {
                            int cmin = Math.Min(p0.x, p1.x);
                            int cmax = Math.Max(p0.x, p1.x);
                            for (int c = cmin; c <= cmax; c++)
                                map[new Pos(c, p0.y)] = '#';
                        }
                        else
                        {
                            int rmin = Math.Min(p0.y, p1.y);
                            int rmax = Math.Max(p0.y, p1.y);
                            for (int r = rmin; r <= rmax; r++)
                                map[new Pos(p0.x, r)] = '#';
                        }
                    }
                }
            }
            return map;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var map = ReadMap(file);
            //Map.Build(map).Print();
            int maxRow = map.Select(z => z.Key.y).Max();
            int a = 0;
            while (AddSand(map, maxRow))
                a++;
            //Map.Build(map).Print();
            map = ReadMap(file);
            int floor = maxRow + 2;
            for (int i = -1000; i < 2000; i++)
                map[new Pos(i, floor)] = '#';
            int b = 0;
            while (AddSand(map, floor))
                b++;
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
