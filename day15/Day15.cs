using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day15
    {
        // Beacon Exclusion Zone: Reduce search space or wait too long
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<(Pos, Pos, int)> sensors = new List<(Pos, Pos, int)>();
            bool example = file == "example.txt";
            int row = example ? 10 : 2000000;
            foreach (var s in input)
            {
                var m = Extract.Ints(s);
                Pos p0 = new Pos(m[0], m[1]);
                Pos p1 = new Pos(m[2], m[3]);
                sensors.Add((p0, p1, p0.ManhattanDistance(p1)));
            }
            int xmin = 0, xmax = 0;
            foreach (var s in sensors)
            {
                xmin = Math.Min(xmin, s.Item1.x - s.Item3);
                xmax = Math.Max(xmax, s.Item1.x + s.Item3);
            }
            int n = 0;
            Console.WriteLine("Min: {0}, max: {1}", xmin, xmax);
            for (int i = xmin; i <= xmax; i++)
            {
                Pos p = new Pos(i, row);
                bool visible = false;
                foreach (var s in sensors)
                {
                    if (s.Item1.ManhattanDistance(p) <= s.Item3)
                        visible = true;
                }
                foreach (var s in sensors)
                {
                    if (s.Item2 == p)
                        visible = false;
                }
                if (visible)
                    n++;
            }
            // B
            HashSet<Pos> toCheck = new HashSet<Pos>();
            foreach (var s in sensors)
            {
                Pos p0 = s.Item1;
                int r = s.Item3 + 1;
                for (int i = 0; i < r; i++)
                {
                    toCheck.Add(new Pos(p0.x + i, p0.y + r - i));
                    toCheck.Add(new Pos(p0.x + r - i, p0.y - i));
                    toCheck.Add(new Pos(p0.x - i, p0.y - r + i));
                    toCheck.Add(new Pos(p0.x - r + i, p0.y + i));
                }
            }
            Pos lonely = new Pos();
            bool CanReach(Pos pA, Pos pB, int rB)
            {
                return pA.ManhattanDistance(pB) <= rB;
            }
            foreach (Pos p in toCheck)
            {
                bool canReach = false;
                foreach (var s in sensors)
                {
                    if (CanReach(p, s.Item1, s.Item3))
                        canReach = true;
                }
                if (!canReach && p.x >= 0 && p.x <= row * 2 && p.y >= 0 && p.y <= row * 2)
                {
                    lonely = p;
                    break;
                }
            }
            Console.WriteLine("x: {0}, y: {1}", lonely.x, lonely.y);
            long b = lonely.x * 4000000L + lonely.y;
            return (n, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
