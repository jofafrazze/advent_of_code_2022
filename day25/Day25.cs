using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day25
    {
        // Full of Hot Air: SNAFU numbering, base 5 and more
        static long SnafuToDecimal(string s)
        {
            long sum = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int offs = s.Length - 1 - i;
                long base_ = (long)Math.Pow(5, i);
                int num = "=-012".IndexOf(s[offs]) - 2;
                sum += num * base_;
            }
            //Console.WriteLine($"SNAFU {s} --> Decimal {sum}");
            return sum;
        }
        static string DecimalToSnafu(long num)
        {
            string s = "";
            long n = num;
            for (int i = 0; n > 0; i++)
            {
                int rest = (int)(n % 5);
                int charPos = (rest + 2) % 5;
                s = Char.ToString("=-012"[charPos]) + s;
                n = (n + 4 - charPos) / 5;
            }
            //Console.WriteLine($"Decimal {num} --> SNAFU {s}");
            return s;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            long sum = 0;
            foreach (var s in ReadInput.Strings(Day, file))
                sum += SnafuToDecimal(s);
            return (DecimalToSnafu(sum), 0);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
