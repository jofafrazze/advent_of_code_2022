using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day10
    {
        // Today: 
        public static void Add(List<int> nums, int position, int cycle, int c, int x)
        {
            if (nums.Count <= position && c >= cycle)
                nums.Add(x * cycle);
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<int> nums = new();
            int nextx = 1, x;
            int c = 0, cmap = 0;
            var m = new Map(40, 6, ' ');
            foreach (var s in input)
            {
                x = nextx;
                if (s == "noop")
                {
                    c += 1;
                }
                else 
                {
                    c += 2;
                    nextx += int.Parse(s[5..]);
                }
                if (nums.Count < 6)
                {
                    Add(nums, 0, 20, c, x);
                    Add(nums, 1, 60, c, x);
                    Add(nums, 2, 100, c, x);
                    Add(nums, 3, 140, c, x);
                    Add(nums, 4, 180, c, x);
                    Add(nums, 5, 220, c, x);
                }
                while (cmap < c)
                {
                    int row = cmap / 40;
                    int col = cmap % 40;
                    int diff = x - col;
                    char fill = (diff > 1 || diff < -1) ? ' ' : '#';
                    m[new Pos(col, row)] = fill;
                    cmap++;
                }
                if (c >= 240)
                    break;
            }
            return (nums.Sum(), m.PrintToString());
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
