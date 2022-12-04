using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day02
    {
        // Rock Paper Scissors: Handle game logics, get confused
        public static (Object, Object) DoPuzzle(string file)
        {
            var round = new List<int>() { 3, 0, 6 };
            var bselect = new List<int>() { -1, 0, 1 };
            var bend = new List<int>() { 0, 3, 6 };
            var input = ReadInput.Strings(Day, file);
            int asum = 0, bsum = 0;
            foreach (var s in input)
            {
                int t = "ABC".IndexOf(s[0]);
                int u = "XYZ".IndexOf(s[2]);
                asum += u + 1 + round[(t - u + 3) % 3];
                int bown = (t + bselect[u] + 3) % 3 + 1;
                bsum += bown + bend[u];
            }
            return (asum, bsum);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
