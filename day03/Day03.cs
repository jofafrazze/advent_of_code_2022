using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day03
    {
        // Rucksack Reorganization: Find intersection between groups
        public static int Priority(char c) => c + (char.IsUpper(c) ? 27 - 'A' : 1 - 'a');
        public static (Object, Object) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            int asum = 0, bsum = 0;
            foreach (var s in input)
            {
                int n = s.Length / 2;
                asum += Priority(s[..n].Intersect(s[n..]).First());

            }
            int pos = 0;
            while (pos < input.Count)
            {
                var g = input.Skip(pos).Take(3).ToList();
                bsum += Priority(g[0].Intersect(g[1]).Intersect(g[2]).First());
                pos += 3;
            }
            return (asum, bsum);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
