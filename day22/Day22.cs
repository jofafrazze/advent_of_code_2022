using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day22
    {
        // Today: 
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            Dictionary<Pos, char> map = new();
            int y = 0;
            foreach (var s in input[0])
            {
                for (int x = 0; x < s.Length; x++)
                    if (s[x] != ' ')
                        map[new Pos(x, y)] = s[x];
                y++;
            }
            List<Pos> dirs = new() { CoordsRC.right, CoordsRC.down, CoordsRC.left, CoordsRC.up };
            string desc = input[1][0];
            var descNums = Extract.Ints(desc);
            var descRots = string.Join("", Extract.RegexRemove(desc, "[^LR]"));
            int nDesc = descNums.Length + descRots.Length;
            int a = 0;
            {
                Pos cur = map.Where(p => p.Key.y == 0).MinBy(p => p.Key.x).Key;
                int facing = 0;
                for (int i = 0; i < nDesc; i++)
                {
                    if (i % 2 == 0)
                    {
                        int steps = descNums[i / 2];
                        Pos dir = dirs[facing];
                        for (int n = 0; n < steps; n++)
                        {
                            Pos next = cur + dir;
                            if (!map.ContainsKey(next))
                            {
                                if (facing == 0)
                                    next = map.Where(p => p.Key.y == cur.y).MinBy(p => p.Key.x).Key;
                                else if (facing == 1)
                                    next = map.Where(p => p.Key.x == cur.x).MinBy(p => p.Key.y).Key;
                                else if (facing == 2)
                                    next = map.Where(p => p.Key.y == cur.y).MaxBy(p => p.Key.x).Key;
                                else
                                    next = map.Where(p => p.Key.x == cur.x).MaxBy(p => p.Key.y).Key;
                            }
                            if (map[next] != '#')
                                cur = next;
                        }
                    }
                    else
                    {
                        facing = (facing + (descRots[i / 2] == 'L' ? 3 : 1)) % dirs.Count;
                    }
                }
                a = (cur.y + 1) * 1000 + (cur.x + 1) * 4 + facing;
            }
            // B
            int b = 0;
            {
                bool example = file == "example.txt";
                int cubeSide = example ? 4 : 50;
                Dictionary<char, List<(char surf, int rotation)>> nextSurface = example ? new()
                {
                    { 'A', new() { ('F', 2), ('B', 0), ('C', 1), ('D', 2) } },
                    { 'B', new() { ('F', 3), ('E', 0), ('C', 0), ('A', 0) } },
                    { 'C', new() { ('B', 0), ('E', 1), ('D', 0), ('A', 3) } },
                    { 'D', new() { ('C', 0), ('E', 2), ('F', 3), ('A', 2) } },
                    { 'E', new() { ('F', 0), ('D', 2), ('C', 3), ('B', 0) } },
                    { 'F', new() { ('A', 2), ('D', 1), ('E', 0), ('B', 1) } },
                } : new()
                {
                    { 'A', new() { ('B', 0), ('C', 0), ('D', 2), ('F', 3) } },
                    { 'B', new() { ('E', 2), ('C', 3), ('A', 0), ('F', 0) } },
                    { 'C', new() { ('B', 1), ('E', 0), ('D', 1), ('A', 0) } },
                    { 'D', new() { ('E', 0), ('F', 0), ('A', 2), ('C', 3) } },
                    { 'E', new() { ('B', 2), ('F', 3), ('D', 0), ('C', 0) } },
                    { 'F', new() { ('E', 1), ('B', 0), ('A', 1), ('D', 0) } },
                };
                Dictionary<char, Pos> unitMapSurfaceToPosition = example ? new()
                {
                    { 'A', new(2, 0) },
                    { 'B', new(2, 1) },
                    { 'C', new(1, 1) },
                    { 'D', new(0, 1) },
                    { 'E', new(2, 2) },
                    { 'F', new(3, 2) },
                } : new() 
                {
                    { 'A', new(1, 0) },
                    { 'B', new(2, 0) },
                    { 'C', new(1, 1) },
                    { 'D', new(0, 2) },
                    { 'E', new(1, 2) },
                    { 'F', new(0, 3) },
                };
                Dictionary<Pos, char> unitMapPositionToSurface = unitMapSurfaceToPosition.ToDictionary(x => x.Value, x => x.Key);
                char MapSurf(Pos p) => unitMapPositionToSurface[new Pos(p.x / cubeSide, p.y / cubeSide)];
                Pos MapPos(char surf)
                {
                    Pos p = unitMapSurfaceToPosition[surf];
                    return new(p.x * cubeSide, p.y * cubeSide);
                }
                Pos cur = map.Where(p => p.Key.y == 0).MinBy(p => p.Key.x).Key;
                int facing = 0;
                for (int i = 0; i < nDesc; i++)
                {
                    if (i % 2 == 0)
                    {
                        int steps = descNums[i / 2];
                        for (int n = 0; n < steps; n++)
                        {
                            Pos dir = dirs[facing];
                            Pos next = cur + dir;
                            int nextFacing = facing;
                            if (!map.ContainsKey(next))
                            {
                                char curSurf = MapSurf(cur);
                                var nextSurf = nextSurface[curSurf][facing];
                                var curLocal = new Pos((cur.x + cubeSide) % cubeSide, (cur.y + cubeSide) % cubeSide);
                                var nextLocalRaw = new Pos((next.x + cubeSide) % cubeSide, (next.y + cubeSide) % cubeSide);
                                var nextLocal = new Pos();
                                int switchRot = (dirs.Count - nextSurf.rotation) % dirs.Count;
                                nextFacing = (facing + switchRot) % dirs.Count;
                                if (switchRot == 0)
                                {
                                    nextLocal = new(nextLocalRaw.x, nextLocalRaw.y);
                                }
                                else if (switchRot == 1)
                                {
                                    nextLocal = new(cubeSide - nextLocalRaw.y - 1, nextLocalRaw.x);
                                }
                                else if (switchRot == 2)
                                {
                                    nextLocal = new(cubeSide - nextLocalRaw.x - 1, cubeSide - nextLocalRaw.y - 1);
                                }
                                else
                                {
                                    nextLocal = new(nextLocalRaw.y, cubeSide - nextLocalRaw.x - 1);
                                }
                                next = MapPos(nextSurf.surf) + nextLocal;
                                if (!map.ContainsKey(next))
                                    throw new Exception("Bug!");
                            }
                            if (map[next] != '#')
                            {
                                cur = next;
                                facing = nextFacing;
                            }
                        }
                    }
                    else
                    {
                        facing = (facing + (descRots[i / 2] == 'L' ? 3 : 1)) % dirs.Count;
                    }
                }
                b = (cur.y + 1) * 1000 + (cur.x + 1) * 4 + facing;
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
