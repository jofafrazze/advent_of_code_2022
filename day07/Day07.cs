using AdventOfCode;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace aoc
{
    public class Day07
    {
        // Today: 

        public static Object PartA(string file)
        {
            var input = ReadInput.Ints(Day, file);
            return 0;
        }

        public static Object PartB(string file)
        {
            var v = ReadInput.Strings(Day, file);
            return 0;
        }

        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
