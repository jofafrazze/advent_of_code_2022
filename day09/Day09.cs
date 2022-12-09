using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day09
    {
        // Rope Bridge: Move a rope's tail(s) so it follows the head
        public static void MoveTail(Pos head, ref Pos tail)
        {
            if (!CoordsXY.Neighbours8(head).Contains(tail))
                tail += new Pos(head.x.CompareTo(tail.x), head.y.CompareTo(tail.y));
        }
        public static int MoveRope(List<string> steps, int length)
        {
            var rope = new Pos[length];
            var visited = new HashSet<Pos>() { rope.Last() };
            foreach (var s in steps)
            {
                Pos dir = CoordsXY.directions4["URDL".IndexOf(s[0])];
                for (int n = 0; n < int.Parse(s[2..]); n++)
                {
                    rope[0] += dir;
                    for (int i = 1; i < length; i++)
                        MoveTail(rope[i - 1], ref rope[i]);
                    visited.Add(rope.Last());
                }
            }
            return visited.Count;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            return (MoveRope(input, 2), MoveRope(input, 10));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
