using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day22
    {
        // Monkey Map: Move around map, wrap around, first in flat 2D then on 3D cube surface
        static Dictionary<Pos, char> map = new();
        static readonly List<Pos> dirs = new() { CoordsRC.right, CoordsRC.down, CoordsRC.left, CoordsRC.up };
        static (Pos pos, int facing) MoveFlat(Pos cur, int facing)
        {
            switch (facing)
            {
                case 0: return (map.Where(p => p.Key.y == cur.y).MinBy(p => p.Key.x).Key, facing);
                case 1: return (map.Where(p => p.Key.x == cur.x).MinBy(p => p.Key.y).Key, facing);
                case 2: return (map.Where(p => p.Key.y == cur.y).MaxBy(p => p.Key.x).Key, facing);
                default: return (map.Where(p => p.Key.x == cur.x).MaxBy(p => p.Key.y).Key, facing);
            }
        }
        static readonly int cubeSideTest = 4;
        static readonly int cubeSideReal = 50;
        static readonly Dictionary<char, List<(char surf, int rotation)>> nextSurfaceTest = new()
        {
            { 'A', new() { ('F', 2), ('B', 0), ('C', 1), ('D', 2) } },
            { 'B', new() { ('F', 3), ('E', 0), ('C', 0), ('A', 0) } },
            { 'C', new() { ('B', 0), ('E', 1), ('D', 0), ('A', 3) } },
            { 'D', new() { ('C', 0), ('E', 2), ('F', 3), ('A', 2) } },
            { 'E', new() { ('F', 0), ('D', 2), ('C', 3), ('B', 0) } },
            { 'F', new() { ('A', 2), ('D', 1), ('E', 0), ('B', 1) } },
        };
        static readonly Dictionary<char, List<(char surf, int rotation)>> nextSurfaceReal = new()
        {
            { 'A', new() { ('B', 0), ('C', 0), ('D', 2), ('F', 3) } },
            { 'B', new() { ('E', 2), ('C', 3), ('A', 0), ('F', 0) } },
            { 'C', new() { ('B', 1), ('E', 0), ('D', 1), ('A', 0) } },
            { 'D', new() { ('E', 0), ('F', 0), ('A', 2), ('C', 3) } },
            { 'E', new() { ('B', 2), ('F', 3), ('D', 0), ('C', 0) } },
            { 'F', new() { ('E', 1), ('B', 0), ('A', 1), ('D', 0) } },
        };
        static readonly Dictionary<char, Pos> unitMapSurfaceToPositionTest = new()
        {
            { 'A', new(2, 0) },
            { 'B', new(2, 1) },
            { 'C', new(1, 1) },
            { 'D', new(0, 1) },
            { 'E', new(2, 2) },
            { 'F', new(3, 2) },
        };
        static readonly Dictionary<char, Pos> unitMapSurfaceToPositionReal = new()
        {
            { 'A', new(1, 0) },
            { 'B', new(2, 0) },
            { 'C', new(1, 1) },
            { 'D', new(0, 2) },
            { 'E', new(1, 2) },
            { 'F', new(0, 3) },
        };
        static int cubeSide;
        static Dictionary<char, List<(char surf, int rotation)>> nextSurface;
        static Dictionary<char, Pos> unitMapSurfaceToPosition;
        static Dictionary<Pos, char> unitMapPositionToSurface;
        static char MapSurf(Pos p) => unitMapPositionToSurface[new Pos(p.x / cubeSide, p.y / cubeSide)];
        static Pos MapPos(char surf)
        {
            Pos p = unitMapSurfaceToPosition[surf];
            return new(p.x * cubeSide, p.y * cubeSide);
        }
        static (Pos pos, int facing) MoveOnCube(Pos cur, int facing)
        {
            Pos dir = dirs[facing];
            Pos next = cur + dir;
            char curSurf = MapSurf(cur);
            var nextSurf = nextSurface[curSurf][facing];
            var nextLocalRaw = new Pos((next.x + cubeSide) % cubeSide, (next.y + cubeSide) % cubeSide);
            var nextLocal = new Pos();
            int switchRot = (dirs.Count - nextSurf.rotation) % dirs.Count;
            switch (switchRot)
            {
                case 0: nextLocal = new(nextLocalRaw.x, nextLocalRaw.y); break;
                case 1: nextLocal = new(cubeSide - nextLocalRaw.y - 1, nextLocalRaw.x); break;
                case 2: nextLocal = new(cubeSide - nextLocalRaw.x - 1, cubeSide - nextLocalRaw.y - 1); break;
                default: nextLocal = new(nextLocalRaw.y, cubeSide - nextLocalRaw.x - 1); break;
            }
            next = MapPos(nextSurf.surf) + nextLocal;
            return (next, (facing + switchRot) % dirs.Count);
        }
        static int WalkMap(int[] nSteps, string rots, bool partA)
        {
            int nDesc = nSteps.Length + rots.Length;
            Pos cur = map.Where(p => p.Key.y == 0).MinBy(p => p.Key.x).Key;
            int facing = 0;
            for (int i = 0; i < nDesc; i++)
                if (i % 2 == 0)
                {
                    int steps = nSteps[i / 2];
                    for (int n = 0; n < steps; n++)
                    {
                        Pos next = cur + dirs[facing];
                        int nextFacing = facing;
                        if (!map.ContainsKey(next))
                            (next, nextFacing) = partA ? MoveFlat(cur, facing) : MoveOnCube(cur, facing);
                        if (map[next] != '#')
                            (cur, facing) = (next, nextFacing);
                    }
                }
                else
                    facing = (facing + (rots[i / 2] == 'L' ? 3 : 1)) % dirs.Count;
            return (cur.y + 1) * 1000 + (cur.x + 1) * 4 + facing;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            int y = 0;
            foreach (var s in input[0])
            {
                for (int x = 0; x < s.Length; x++)
                    if (s[x] != ' ')
                        map[new Pos(x, y)] = s[x];
                y++;
            }
            string desc = input[1][0];
            var descNums = Extract.Ints(desc);
            var descRots = string.Join("", Extract.RegexRemove(desc, "[^LR]"));
            int nDesc = descNums.Length + descRots.Length;
            
            bool example = file == "example.txt";
            cubeSide = example ? cubeSideTest : cubeSideReal;
            nextSurface = example ? nextSurfaceTest : nextSurfaceReal;
            unitMapSurfaceToPosition = example ?
                unitMapSurfaceToPositionTest : unitMapSurfaceToPositionReal;
            unitMapPositionToSurface = unitMapSurfaceToPosition.ToDictionary(x => x.Value, x => x.Key);
            
            int a = WalkMap(descNums, descRots, true);
            int b = WalkMap(descNums, descRots, false);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
