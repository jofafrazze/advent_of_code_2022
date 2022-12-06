using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day06
    {
        // Tuning Trouble: Find unique substring of certain length
        public static int FindUnique(string s, int nUnique)
        {
            var z = new List<char>();
            int n = 0;
            foreach (char c in s)
            {
                n++;
                z.Add(c);
                if (z.Count > nUnique)
                    z.RemoveAt(0);
                if ((new HashSet<char>(z)).Count == nUnique)
                    break;
            }
            return n;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var s = ReadInput.Strings(Day, file)[0];
            return (FindUnique(s, 4), FindUnique(s, 14));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
