using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day09
    {
        // Rope Bridge: Move a rope's tail(s) so it follows the head
        public static Pos MoveTail(Pos head, Pos tail)
        {
            bool moveTail = true;
            foreach (var d in CoordsXY.directions8)
                if (head + d == tail)
                    moveTail = false;
            if (moveTail)
            {
                int xadd = head.x == tail.x ? 0 : (head.x > tail.x ? 1 : -1);
                int yadd = head.y == tail.y ? 0 : (head.y > tail.y ? 1 : -1);
                tail.x += xadd;
                tail.y += yadd;
            }
            return tail;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var visited = new HashSet<Pos>();
            Pos head = new Pos(), tail = new Pos();
            visited.Add(tail);
            foreach (var s in input)
            {
                Pos dir = new Pos();
                if (s[0] == 'L')
                    dir = CoordsXY.goLeft;
                else if (s[0] == 'U')
                    dir = CoordsXY.goUp;
                else if (s[0] == 'R')
                    dir = CoordsXY.goRight;
                else if (s[0] == 'D')
                    dir = CoordsXY.goDown;
                int n = int.Parse(s.Substring(2));
                for (int nn = 0; nn < n; nn++)
                {
                    head += dir;
                    tail = MoveTail(head, tail);
                    visited.Add(tail);
                }
            }
            // B
            Pos[] rope = new Pos[10];
            var bvisited = new HashSet<Pos>();
            bvisited.Add(rope[9]);
            for (int i = 0; i < rope.Length; i++)
                rope[i] = new Pos();
            foreach (var s in input)
            {
                Pos dir = new Pos();
                if (s[0] == 'L')
                    dir = CoordsXY.goLeft;
                else if (s[0] == 'U')
                    dir = CoordsXY.goUp;
                else if (s[0] == 'R')
                    dir = CoordsXY.goRight;
                else if (s[0] == 'D')
                    dir = CoordsXY.goDown;
                int n = int.Parse(s.Substring(2));
                for (int nn = 0; nn < n; nn++)
                {
                    rope[0] += dir;
                    for (int i = 0; i < rope.Length - 1; i++)
                        rope[i + 1] = MoveTail(rope[i], rope[i + 1]);
                    bvisited.Add(rope[9]);
                }
            }
            return (visited.Count, bvisited.Count);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
