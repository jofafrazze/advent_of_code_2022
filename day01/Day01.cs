using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day01
    {
        // Calorie Counting: Sum groups of integers, find max

        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            input.Add("");
            List<int> group = new();
            int sum = 0;
            foreach (var s in input)
                if (s.Length == 0)
                {
                    group.Add(sum);
                    sum = 0;
                }
                else
                    sum += Int32.Parse(s);
            var sorted = group.OrderByDescending(x => x);
            return (sorted.First(), sorted.Take(3).Sum());
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
