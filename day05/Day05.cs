using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day05
    {
        // Supply Stacks: Parse tricky input, work with stacks
        public static string FixCrates(List<Stack<char>> cratesIn, List<int[]> steps, bool oneByOne)
        {
            var crates = cratesIn.Select(c => new Stack<char>(c)).ToList();
            foreach (var s in steps)
            {
                string a = "";
                for (int i = 0; i < s[0]; i++)
                    a += crates[s[1] - 1].Pop();
                for (int i = 0; i < s[0]; i++)
                    crates[s[2] - 1].Push(a[oneByOne ? i : a.Length - 1 - i]);
            }
            return new string(crates.Select(s => s.Pop()).ToArray());
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            var cratesInput = input[0].AsEnumerable().Reverse().Skip(1).ToList();
            var cratesLists = Extract.Transpose(cratesInput.Select(s => s.Where((c, i) => (i - 1) % 4 == 0)));
            var crates = cratesLists.Select(s => new Stack<char>(s.Where(c => c != ' ').Reverse())).ToList();
            var steps = input[1].Select(s => Extract.Ints(s)).ToList();
            return (FixCrates(crates, steps, true), FixCrates(crates, steps, false));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
