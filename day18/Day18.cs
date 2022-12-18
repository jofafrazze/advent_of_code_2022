using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition3D<int>;

namespace aoc
{
    public class Day18
    {
        // Today: 
        static List<Pos> dir6 = new()
        {
            new Pos(-1, 0, 0), new Pos(1, 0, 0), new Pos(0, -1, 0),
            new Pos(0, 1, 0), new Pos(0, 0, -1), new Pos(0, 0, 1)
        };
        static int Surface(List<Pos> cubes)
        {
            int n = 0;
            foreach (var c in cubes)
            {
                var n6 = dir6.Select(w => w + c);
                var others = cubes.Where(w => w != c);
                int covered = others.Intersect(n6).Count();
                n += 6 - covered;
            }
            return n;
        }
        static HashSet<Pos> FindEncapsulation(Pos p0, List<Pos> cubes)
        {
            int xMin = cubes.Select(w => w.x).Min();
            int xMax = cubes.Select(w => w.x).Max();
            int yMin = cubes.Select(w => w.y).Min();
            int yMax = cubes.Select(w => w.y).Max();
            int zMin = cubes.Select(w => w.z).Min();
            int zMax = cubes.Select(w => w.z).Max();
            HashSet<Pos> within = new() { p0 };
            List<Pos> toTry = new() { p0 };
            while (toTry.Any())
            {
                Pos p = toTry.First();
                toTry.RemoveAt(0);
                if (p.x <= xMin || p.x >= xMax || p.y <= yMin || p.y >= yMax || p.z <= zMin || p.z >= zMax)
                    return new HashSet<Pos>();
                else
                {
                    var n6 = dir6.Select(w => w + p);
                    var canReach = n6.Except(cubes.Intersect(n6));
                    foreach (var c in canReach)
                        if (!within.Contains(c))
                        {
                            within.Add(c);
                            toTry.Add(c);
                        }
                }
            }
            return within;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            List<Pos> cubes = new();
            foreach (var s in ReadInput.Strings(Day, file))
            {
                var v = Extract.Ints(s);
                cubes.Add(new Pos(v[0], v[1], v[2]));
            }
            int a = Surface(cubes);
            HashSet<Pos> within = new();
            int xMin = cubes.Select(w => w.x).Min();
            int xMax = cubes.Select(w => w.x).Max();
            int yMin = cubes.Select(w => w.y).Min();
            int yMax = cubes.Select(w => w.y).Max();
            int zMin = cubes.Select(w => w.z).Min();
            int zMax = cubes.Select(w => w.z).Max();
            for (int x = xMin; x <= xMax; x++)
            {
                Console.WriteLine("x = {0}({1})", x, xMax);
                for (int y = yMin; y <= yMax; y++)
                {
                    for (int z = zMin; z <= zMax; z++)
                    {
                        Pos p = new(x, y, z);
                        if (!cubes.Contains(p))
                        {
                            HashSet<Pos> w = FindEncapsulation(p, cubes);
                            within.UnionWith(w);
                        }
                    }
                }
            }
            return (a, a - Surface(within.ToList()));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
