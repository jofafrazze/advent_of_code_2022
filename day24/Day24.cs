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
        static List<HashSet<Pos>> CreateBlizzards(Map map)
        {
            var validPos = map.Positions().Where(w => map[w] != '#').ToHashSet();
            List<List<Pos>> GenerateBlizzards1D(char c1, char c2, int n)
            {
                var initialBlizzards = validPos.Where(w => map[w] == c1 || map[w] == c2);
                var valley = initialBlizzards.ToDictionary(w => w, w => new List<char>() { map[w] });
                var b = new List<List<Pos>>(n);
                for (int i = 0; i < n; i++)
                {
                    b.Add(valley.Keys.ToList());
                    valley = StepBlizzards(valley, map);
                }
                return b;
            }
            int w = map.width - 2;
            int h = map.height - 2;
            var blizzardsHz = GenerateBlizzards1D('<', '>', w);
            var blizzardsVc = GenerateBlizzards1D('^', 'v', h);
            int lcm = (int)Utils.LCM(w, h);
            var blizzards = new List<HashSet<Pos>>(lcm);
            for (int n = 0; n < lcm; n++)
                blizzards.Add(blizzardsHz[n % w].Union(blizzardsVc[n % h]).ToHashSet());
            return blizzards;
        }
        static int WalkMap(Pos start, Pos end, int startMinute, Map map, List<HashSet<Pos>> blizzards)
        {
            var validPos = map.Positions().Where(w => map[w] != '#').ToHashSet();
            var visited = new HashSet<(Pos, int)>() { (start, startMinute) };
            var toTry = new PriorityQueue<(Pos p, int minute), int>(new ((Pos, int), int)[] { ((start, startMinute), 0) });
            //PrintValley(valley, map, positions, i);
            int w = map.width - 2;
            int h = map.height - 2;
            while (toTry.TryDequeue(out var v, out int _))
            {
                if (v.p == end)
                    return v.minute;
                var valley = blizzards[(v.minute + 1) % blizzards.Count];
                foreach (var d in dirs)
                {
                    Pos np = v.p + d;
                    if (!valley.Contains(np) && validPos.Contains(np))
                        if (visited.Add((np, v.minute + 1)))
                            toTry.Enqueue((np, v.minute + 1), v.minute + 1 + v.p.ManhattanDistance(end));
                }
                //PrintValley(valley, map, positions, i);
            }
            throw new Exception("No path to end found!");
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
