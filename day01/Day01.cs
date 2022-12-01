using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day01
    {
        // Calorie Counting: Sum groups of integers, find max

        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            int max = 0;
            int sum = 0;
            foreach(var s in input)
            {
                if (s.Length == 0)
                    sum = 0;
                else
                {
                    sum += Int32.Parse(s);
                    if (sum > max)
                        max = sum;
                }
            }
            return max;
        }

        public static Object PartB(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<int> max = new();
            int sum = 0;
            foreach (var s in input)
            {
                if (s.Length == 0)
                {
                    max.Add(sum);
                    sum = 0;
                }
                else
                    sum += Int32.Parse(s); 
            }
            return max.OrderByDescending(x => x).Take(3).Sum();
        }

        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
