using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day10
    {
        // Cathode-Ray Tube: Run assembly program, create ascii art
        public static void AddSignalStrength(List<int> nums, int pos, int signalCycle, int cycle, int x)
        {
            if (nums.Count <= pos && cycle >= signalCycle)
                nums.Add(x * signalCycle);
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            List<int> nums = new();
            int x, nextx = 1, cycle = 0, c = 0;
            var m = new Map(40, 6, ' ');
            foreach (var s in ReadInput.Strings(Day, file))
            {
                x = nextx;
                cycle += s == "noop" ? 1 : 2;
                nextx += s == "noop" ? 0 : int.Parse(s[5..]);
                for (int i = 0; i < 6; i++)
                    AddSignalStrength(nums, i, i * 40 + 20, cycle, x);
                while (c < cycle)
                {
                    int row = c / 40;
                    int col = c % 40;
                    m[new Pos(col, row)] = Math.Abs(x - col) > 1 ? ' ' : '#';
                    c++;
                }
            }
            return (nums.Sum(), m.PrintToString());
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
