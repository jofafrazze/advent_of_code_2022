using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day08
    {
        // Treetop Tree House: Do metrics in all positions & directions
        public static int ViewLength(Map m, Pos p0, Pos dir, HashSet<Pos> visible)
        {
            bool isVisible = true;
            int viewLength = -1;
            for (var p = p0; m.HasPosition(p);)
            {
                viewLength++;
                if (m[p] >= m[p0] && p != p0)
                {
                    isVisible = false;
                    break;
                }
                p += dir;
            }
            if (isVisible)
                visible.Add(p0);
            return viewLength;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var m = Map.Build(ReadInput.Strings(Day, file));
            HashSet<Pos> visible = new();
            int max = m.Positions()
                .Select(p => CoordsXY.directions4.Select(dir => ViewLength(m, p, dir, visible))
                .Aggregate((a, x) => a * x))
                .Max();
            return (visible.Count, max);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
