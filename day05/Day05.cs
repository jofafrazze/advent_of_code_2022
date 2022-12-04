using AdventOfCode;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace aoc
{
    public class Day05
    {
        // Today: 

        public static (Object a, Object b) DoPuzzle(string file)
        {
            //var z = ReadData(file);
            var z = ReadInput.Strings(Day, file);
            //Console.WriteLine("A is {0}", a);
            return (0, 0);
        }

        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
