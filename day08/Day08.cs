using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day08
    {
        // Treetop Tree House: Do metrics in all positions & directions
        public static bool Visible(Map m, Pos p0, Pos fromDir)
        {
            bool visible = true;
            var p = p0;
            do
            {
                if (p != p0 && m.HasPosition(p))
                {
                    if (m[p] >= m[p0])
                    {
                        visible = false;
                        break;
                    }
                }
                p = p + fromDir;
            }
            while (m.HasPosition(p));
            return visible;
        }
        public static int VisiblePartB(Map m, Pos p0, Pos toDir)
        {
            int nVisible = 0;
            var p = p0;
            do
            {
                if (p != p0 && m.HasPosition(p))
                {
                    nVisible++;
                    if (m[p] >= m[p0])
                        break;
                }
                p = p + toDir;
            }
            while (m.HasPosition(p));
            return nVisible;
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var m = Map.Build(input);
            HashSet<Pos> visible = new HashSet<Pos>();
            foreach (Pos p in m.Positions())
            {
                if (Visible(m, p, CoordsXY.goLeft))
                    visible.Add(p);
                if (Visible(m, p, CoordsXY.goUp))
                    visible.Add(p);
                if (Visible(m, p, CoordsXY.goRight))
                    visible.Add(p);
                if (Visible(m, p, CoordsXY.goDown))
                    visible.Add(p);
            }
            return visible.Count;
        }

        public static Object PartB(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var m = Map.Build(input);
            int max = 0;
            foreach (Pos p in m.Positions())
            {
                int a = VisiblePartB(m, p, CoordsXY.goLeft);
                int b = VisiblePartB(m, p, CoordsXY.goUp);
                int c = VisiblePartB(m, p, CoordsXY.goRight);
                int d = VisiblePartB(m, p, CoordsXY.goDown);
                int val = a * b * c * d;
                if (val > max)
                    max = val;
            }
            return max;
        }

        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
