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
            int b = 0;
            bool allStable = false;
            for (; !allStable; b++)
            {
                List<Pos> nextMap = new();
                List<Pos> notProposed = new();
                Dictionary<Pos, List<Pos>> proposed = new();
                List<Pos> proposeToMove = new();
                // Any neighbours i 8 directions?
                foreach (Pos p in map)
                {
                    bool haveNeighbour = false;
                    for (int i = 0; i < CoordsRC.directions8.Count && !haveNeighbour; i++)
                        if (map.Contains(p + CoordsRC.directions8[i]))
                            haveNeighbour = true;
                    if (haveNeighbour)
                        proposeToMove.Add(p);
                    else
                        nextMap.Add(p);
                }
                if (!proposeToMove.Any())
                    allStable = true;
                // Build move propositions for those with neighbours
                foreach (Pos p in proposeToMove)
                {
                    // Try all 4 move directions
                    bool didPropose = false;
                    for (int dd = 0; dd < dirs.Count && !didPropose; dd++)
                    {
                        var ds = dirs[(dd + b) % dirs.Count];
                        bool canMove = true;
                        foreach (Pos d in ds)
                            if (map.Contains(p + d))
                                canMove = false;
                        if (canMove)
                        {
                            Pos pNext = p + ds[0];
                            if (!proposed.ContainsKey(pNext))
                                proposed[pNext] = new();
                            proposed[pNext].Add(p);
                            didPropose = true;
                        }
                    }
                    if (!didPropose)
                        notProposed.Add(p);
                }
                // Move the ones who could move
                List<Pos> notMoved = new();
                foreach (var (k, v) in proposed)
                {
                    if (v.Count == 1)
                        nextMap.Add(k);
                    else
                        foreach (Pos p in v)
                            notMoved.Add(p);
                }
                nextMap.AddRange(notMoved);
                nextMap.AddRange(notProposed);
                map = nextMap.ToHashSet();
                if (b == 10)
                {
                    int w = map.Select(p => p.x).Max() - map.Select(p => p.x).Min() + 1;
                    int h = map.Select(p => p.y).Max() - map.Select(p => p.y).Min() + 1;
                    a = w * h - map.Count;
                }
            }
            //Console.WriteLine("Map contains {0} positions", map.Count);
            int mw = map.Select(p => p.x).Max() - map.Select(p => p.x).Min() + 1;
            int mh = map.Select(p => p.y).Max() - map.Select(p => p.y).Min() + 1;
            //Console.WriteLine("Map surface has {0} positions in total", mw * mh);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
