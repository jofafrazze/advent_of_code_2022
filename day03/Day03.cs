using AdventOfCode;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace aoc
{
    public class Day03
    {
        // Today: 

        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            int sum = 0;
            int n = 0;
            foreach (var item in input)
            {
                n++;
                int len = item.Length / 2;
                var a = item.Substring(0, len);
                var b = item[len..];
                foreach (char c in a)
                {
                    if (b.Contains(c))
                    {
                        int up = c - 'A' + 27;
                        int lo = c - 'a' + 1;
                        sum += char.IsUpper(c) ? up : lo;
                        break;
                    }
                }
            }
            return sum;
        }

        public static Object PartB(string file)
        {
            var input = ReadInput.Strings(Day, file);
            int sum = 0;
            int n = 0;
            var todo = new List<String>(input);
            while (todo.Count > 0)
            {
                n++;
                var group = todo.Take(3).ToList();
                todo.RemoveRange(0, 3);
                foreach (char c in group[0])
                {
                    if (group[1].Contains(c) && group[2].Contains(c))
                    {
                        int up = c - 'A' + 27;
                        int lo = c - 'a' + 1;
                        sum += char.IsUpper(c) ? up : lo;
                        break;
                    }
                }
            }
            return sum;
        }

        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
