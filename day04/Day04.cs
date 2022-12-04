using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day04
    {
        // Camp Cleanup: Find ranges that do (or do not) overlap
        public static (Object, Object) DoPuzzle(string file)
        {
            int asum = 0, bsum = 0;
            foreach (var s in ReadInput.Strings(Day, file))
            {
                var v = s.Split('-', ',').Select(int.Parse).ToList();
                asum += ((v[0] >= v[2] && v[1] <= v[3]) || (v[0] <= v[2] && v[1] >= v[3])) ? 1 : 0;
                bsum += (v[1] < v[2] || v[0] > v[3]) ? 0 : 1;
            }
            return (asum, bsum);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
