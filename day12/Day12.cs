using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day12
    {
        // Hill Climbing Algorithm: Traverse map, first up then down
        public static Dictionary<Pos, int> WalkMap(Map m, Pos start, bool walkUp)
        {
            bool CanMove(int to, int from) => (to - from) * (walkUp ? 1 : -1) <= 1;
            var steps = new Dictionary<Pos, int> { [start] = 0 };
            var next = new Queue<Pos>(new[] { start });
            while (next.Any())
            {
                Pos p = next.Dequeue();
                foreach (var n in CoordsRC.Neighbours4(p))
                    if (m.HasPosition(n) && CanMove(m[n], m[p]))
                        if (!steps.ContainsKey(n) || steps[n] > steps[p] + 1)
                        {
                            steps[n] = steps[p] + 1;
                            next.Enqueue(n);
                        }
            }
            return steps;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var m = Map.Build(ReadInput.Strings(Day, file));
            Pos sPos = m.Positions().Where(p => m[p] == 'S').First();
            Pos ePos = m.Positions().Where(p => m[p] == 'E').First();
            m[sPos] = 'a';
            m[ePos] = 'z';
            int a = WalkMap(m, sPos, true)[ePos];
            int b = WalkMap(m, ePos, false).Where(z => m[z.Key] == 'a').Select(z => z.Value).Min();
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
