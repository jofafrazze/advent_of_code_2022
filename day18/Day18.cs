using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition3D<int>;

namespace aoc
{
    public class Day18
    {
        // Boiling Boulders: Calculate total 3D surface, with and without cavities
        static readonly List<Pos> dir6 = new() 
        {
            new(-1, 0, 0), new(1, 0, 0), new(0, -1, 0), new(0, 1, 0), new(0, 0, -1), new(0, 0, 1)
        };
        static int Surface(List<Pos> cubes)
        {
            int n = 0;
            foreach (var c in cubes)
                n += 6 - cubes.Where(w => w != c).Intersect(dir6.Select(w => w + c)).Count();
            return n;
        }
        static List<Pos> FindEncapsulations(List<Pos> cubes)
        {
            int xMin = cubes.Select(w => w.x).Min();
            int xMax = cubes.Select(w => w.x).Max();
            int yMin = cubes.Select(w => w.y).Min();
            int yMax = cubes.Select(w => w.y).Max();
            int zMin = cubes.Select(w => w.z).Min();
            int zMax = cubes.Select(w => w.z).Max();
            HashSet<Pos> searchSpace = new();
            for (int x = xMin; x <= xMax; x++)
                for (int y = yMin; y <= yMax; y++)
                    for (int z = zMin; z <= zMax; z++)
                        searchSpace.Add(new(x, y, z));
            HashSet<Pos> inside = new();
            var toTry = searchSpace.Except(cubes).ToHashSet();
            while (toTry.Any())
            {
                var cluster = Reachable(toTry.First(), cubes, toTry);
                bool within = 
                    cluster.Select(w => w.x).Min() > xMin && cluster.Select(w => w.x).Max() < xMax &&
                    cluster.Select(w => w.y).Min() > yMin && cluster.Select(w => w.y).Max() < yMax &&
                    cluster.Select(w => w.z).Min() > zMin && cluster.Select(w => w.z).Max() < zMax;
                if (within)
                    inside.UnionWith(cluster);
                toTry = toTry.Except(cluster).ToHashSet();
            }
            return inside.ToList();
        }
        static HashSet<Pos> Reachable(Pos p0, List<Pos> cubes, HashSet<Pos> searchSpace)
        {
            HashSet<Pos> canReach = new();
            Queue<Pos> toTry = new(new[] { p0 });
            while (toTry.Any())
            {
                Pos p = toTry.Dequeue();
                if (!canReach.Contains(p) && searchSpace.Contains(p))
                {
                    canReach.Add(p);
                    var n6 = dir6.Select(w => w + p);
                    foreach (var n in n6.Except(cubes.Intersect(n6)))
                        toTry.Enqueue(n);
                }
            }
            return canReach;
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
            return (a, a - Surface(FindEncapsulations(cubes)));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
