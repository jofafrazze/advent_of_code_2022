using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;
using Range = AdventOfCode.IntRange;

namespace aoc
{
    public class Day15
    {
        // Beacon Exclusion Zone: Reduce search space or wait too long
        public static (Object a, Object b) DoPuzzle(string file)
        {
            List<(Pos ps, Pos pb, int rs)> sensors = new();
            int row = file == "example.txt" ? 10 : 2000000;
            foreach (var s in ReadInput.Strings(Day, file))
            {
                var m = Extract.Ints(s);
                Pos ps = new(m[0], m[1]);
                Pos pb = new(m[2], m[3]);
                sensors.Add((ps, pb, ps.ManhattanDistance(pb)));
            }
            List<Range> ranges = new();
            foreach (var (ps, _, rs) in sensors)
                if (ps.y - rs <= row && ps.y + rs >= row)
                {
                    int xoffs = rs - Math.Abs(ps.y - row);
                    ranges.Add(new(ps.x - xoffs, ps.x + xoffs));
                }
            ranges = Range.NormalizeUnion(ranges);
            foreach (var (_, pb, _) in sensors)
            {
                List<Range> nextRanges = new();
                foreach (Range r in ranges)
                    nextRanges.AddRange(pb.y == row ? r.Except(pb.x) : new() { r });
                ranges = nextRanges;
            }
            int a = ranges.Select(w => w.Size()).Sum();
            //Console.WriteLine($"A: {n}");
            // B
            List<Pos> toCheck = new();
            foreach (var (p, _, rs) in sensors)
            {
                int r = rs + 1;
                for (int i = 0; i < r; i++)
                {
                    toCheck.Add(new Pos(p.x + i, p.y + r - i));
                    toCheck.Add(new Pos(p.x - i, p.y - r + i));
                    toCheck.Add(new Pos(p.x + r - i, p.y - i));
                    toCheck.Add(new Pos(p.x - r + i, p.y + i));
                }
            }
            //Console.WriteLine($"B: Checking {toCheck.Count} points...");
            Pos pd = new();
            var z = sensors;
            foreach (Pos p in toCheck)
            {
                bool canReach = false;
                for (int i = 0; i < z.Count && !canReach; i++)
                    if (Math.Abs(p.x - z[i].ps.x) + Math.Abs(p.y - z[i].ps.y) <= z[i].rs)
                        canReach = true;
                if (!canReach && p.x >= 0 && p.x <= row * 2 && p.y >= 0 && p.y <= row * 2)
                {
                    pd = p;
                    break;
                }
            }
            //Console.WriteLine("x: {0}, y: {1}", pd.x, pd.y);
            long b = pd.x * 4000000L + pd.y;
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
