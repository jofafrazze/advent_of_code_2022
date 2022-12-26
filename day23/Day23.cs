using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day23
    {
        // Unstable Diffusion: Do game of life, but for star fruits
        public static (Object a, Object b) DoPuzzle(string file)
        {
            List<List<Pos>> dirs = new() {
                new() { CoordsRC.up, CoordsRC.upLeft, CoordsRC.upRight },
                new() { CoordsRC.down, CoordsRC.downLeft, CoordsRC.downRight },
                new() { CoordsRC.left, CoordsRC.upLeft, CoordsRC.downLeft },
                new() { CoordsRC.right, CoordsRC.upRight, CoordsRC.downRight }
            };
            HashSet<Pos> map = new();
            var mapmap = Map.Build(ReadInput.Strings(Day, file));
            foreach (Pos p in mapmap.Positions())
                if (mapmap[p] == '#')
                    map.Add(p);
            int a = 0;
            int i = 0;
            bool allStable = false;
            for (; !allStable; i++)
            {
                HashSet<Pos> nextMap = new();
                HashSet<Pos> notProposed = new();
                Dictionary<Pos, HashSet<Pos>> proposed = new();
                HashSet<Pos> proposeToMove = new();
                // Any neighbours i 8 directions?
                foreach (Pos p in map)
                    if (CoordsRC.Neighbours8(p).Intersect(map).Any())
                        proposeToMove.Add(p);
                    else
                        nextMap.Add(p);
                if (!proposeToMove.Any())
                    allStable = true;
                // Build move propositions for those with neighbours
                foreach (Pos p in proposeToMove)
                {
                    // Try all 4 move directions
                    bool didPropose = false;
                    for (int dd = 0; dd < dirs.Count && !didPropose; dd++)
                    {
                        var ds = dirs[(dd + i) % dirs.Count];
                        bool canMove = true;
                        foreach (Pos d in ds)
                            if (map.Contains(p + d))
                                canMove = false;
                        if (canMove)
                        {
                            Pos pNext = p + ds[0];
                            if (!proposed.ContainsKey(pNext))
                                proposed[pNext] = new HashSet<Pos>();
                            proposed[pNext].Add(p);
                            didPropose = true;
                        }
                    }
                    if (!didPropose)
                        notProposed.Add(p);
                }
                // Move the ones who could move
                HashSet<Pos> notMoved = new();
                foreach (var (k, v) in proposed)
                {
                    if (v.Count == 1)
                        nextMap.Add(k);
                    else
                        foreach (Pos p in v)
                            notMoved.Add(p);
                }
                nextMap.UnionWith(notMoved);
                nextMap.UnionWith(notProposed);
                map = nextMap;
                if (i == 10)
                {
                    int w = map.Select(p => p.x).Max() - map.Select(p => p.x).Min() + 1;
                    int h = map.Select(p => p.y).Max() - map.Select(p => p.y).Min() + 1;
                    a = w * h - map.Count;
                }
            }
            return (a, i);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
