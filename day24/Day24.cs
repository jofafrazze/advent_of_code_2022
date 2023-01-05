using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day24
    {
        // Blizzard Basin: Move over surface with moving obstacles
        static void PrintValley(Dictionary<Pos, List<char>> valley, Map map, HashSet<Pos> positions, int steps)
        {
            Map m = new(map);
            m.Positions().Where(w => m[w] != '#').ToList().ForEach(w => m[w] = '.');
            foreach (var p in positions)
                m[p] = 'E';
            foreach (var (p, b) in valley)
                m[p] = b.Count == 1 ? b[0] : b.Count.ToString()[0];
            Console.WriteLine($"Round {steps}: {positions.Count} possible positions");
            m.Print();
        }
        static Dictionary<Pos, List<char>> StepBlizzards(Dictionary<Pos, List<char>> valley, Map map)
        {
            int vWidth = map.width - 2;
            int vHeight = map.height - 2;
            int vxMin = 1;
            int vxMax = map.width - 2;
            int vyMin = 1;
            int vyMax = map.height - 2;
            var nextValley = new Dictionary<Pos, List<char>>();
            foreach (var (pp, b) in valley)
            {
                foreach (var c in b)
                {
                    //Pos newPos = p + dirs[bChars.IndexOf(c)];
                    Pos p = pp;
                    if (c == '^')
                        p.y--;
                    else if (c == '>')
                        p.x++;
                    else if (c == 'v')
                        p.y++;
                    else if (c == '<')
                        p.x--;
                    if (p.x < vxMin)
                        p.x += vWidth;
                    else if (p.x > vxMax)
                        p.x -= vWidth;
                    else if (p.y < vyMin)
                        p.y += vHeight;
                    else if (p.y > vyMax)
                        p.y -= vHeight;
                    if (!nextValley.ContainsKey(p))
                        nextValley[p] = new List<char>(4) { c };
                    else
                        nextValley[p].Add(c);
                }
            }
            return nextValley;
        }
        static string bChars = "^>v<";
        static List<Pos> dirs = new() { CoordsRC.up, CoordsRC.right, CoordsRC.down, CoordsRC.left, new Pos(0, 0) };
        static List<Dictionary<Pos, List<char>>> CreateBlizzards(Map map)
        {
            var validPos = map.Positions().Where(w => map[w] != '#').ToHashSet();
            var initialBlizzards = validPos.Where(w => map[w] != '.');
            var valley = initialBlizzards.ToDictionary(w => w, w => new List<char>() { map[w] });
            int w = map.width - 2;
            int h = map.height - 2;
            int lcm = (int)Utils.LCM(w, h);
            var blizzards = new List<Dictionary<Pos, List<char>>>(lcm) { valley };
            for (int n = 1; n < lcm; n++)
                blizzards.Add(StepBlizzards(blizzards.Last(), map));
            return blizzards;
        }
        static int WalkMap(Pos start, Pos end, int startMinute, Map map, List<Dictionary<Pos, List<char>>> blizzards)
        {
            var validPos = map.Positions().Where(w => map[w] != '#').ToHashSet();
            var positions = new HashSet<Pos>() { start };
            bool done = false;
            //PrintValley(valley, map, positions, i);
            int w = map.width - 2;
            int h = map.height - 2;
            int i = startMinute;
            while (!done)
            {
                i++;
                var valley = blizzards[i % blizzards.Count];
                var nextPositions = new HashSet<Pos>();
                foreach (var p in positions)
                    foreach (var d in dirs)
                    {
                        Pos np = p + d;
                        if (!valley.ContainsKey(np) && validPos.Contains(np))
                        {
                            nextPositions.Add(np);
                            if (np == end)
                                done = true;
                        }
                    }
                positions = nextPositions;
                //PrintValley(valley, map, positions, i);
            }
            return i;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var map = Map.Build(input);
            var blizzards = CreateBlizzards(map);
            Pos start = new(1, 0);
            Pos end = new(map.width - 2, map.height - 1);
            int a = WalkMap(start, end, 0, map, blizzards);
            int b0 = WalkMap(end, start, a, map, blizzards);
            int b = WalkMap(start, end, b0, map, blizzards);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
