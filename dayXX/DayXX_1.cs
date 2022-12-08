using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class DayXX_1
    {
        // Today: 
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            return (0, 0);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
