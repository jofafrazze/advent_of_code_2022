using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day17
    {
        // Pyroclastic Flow: Play Tetris, then find repeating pattern
        static List<Pos> MoveRock(List<Pos> r, int x, int y)
        {
            List<Pos> l = new();
            foreach (Pos p in r)
                l.Add(new Pos(p.x + x, p.y + y));
            return l;
        }
        static bool ValidSidePosition(List<Pos> r, HashSet<Pos> positions)
        {
            bool valid = true;
            foreach (var p in r)
            {
                if (p.x < 0 || p.x > 6)
                    valid = false;
            }
            return valid && ValidDownPosition(r, positions);
        }
        static bool ValidDownPosition(List<Pos> r, HashSet<Pos> positions)
        {
            bool valid = true;
            foreach (var p in r)
            {
                if (positions.Contains(p))
                    valid = false;
            }
            return valid;
        }
        static int FindPatternCycle(List<string> movements, Dictionary<string, List<int>> mPos, int len = 10)
        {
            if (movements.Count < 100 + len)
                return -1;
            int idx0 = movements.Count - 1;
            string find = movements[idx0];
            var idxs = mPos[find].SkipLast(1);
            foreach (int idx in idxs)
            {
                bool found = idx >= len;
                for (int i = 1; i < len && found; i++)
                {
                    if (movements[idx - i] != movements[idx0 - i])
                        found = false;
                }
                if (found)
                {
                    //Console.WriteLine("Index {0} and down {1} elems matches index: {2} (delta: {3})", idx, len, idx0, idx0 - idx);
                    return idx0 - idx;
                }
            }
            return -1;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            string jets = ReadInput.Strings(Day, file)[0];
            List<Pos> sym0 = new() { new(0, 0), new(1, 0), new(2, 0), new(3, 0) }; // -
            List<Pos> sym1 = new() { new(1, 0), new(0, 1), new(1, 1), new(2, 1), new(1, 2) }; // +
            List<Pos> sym2 = new() { new(0, 0), new(1, 0), new(2, 0), new(2, 1), new(2, 2) }; // L
            List<Pos> sym3 = new() { new(0, 0), new(0, 1), new(0, 2), new(0, 3) }; // |
            List<Pos> sym4 = new() { new(0, 0), new(0, 1), new(1, 0), new(1, 1) }; // #
            List<List<Pos>> rocks = new() { sym0, sym1, sym2, sym3, sym4 };
            List<Pos> bottom = new() { new(0, -1), new(1, -1), new(2, -1), new(3, -1), new(4, -1), new(5, -1), new(6, -1) }; // -------
            HashSet<Pos> positions = new(bottom);
            int startHeight;
            int jetIdx = 0;
            long iMax = 1000000000000;
            List<string> movements = new();
            List<int> heights = new();
            Dictionary<string, List<int>> movementPositions = new();
            long b = 0;
            for (int i = 0; i < 2022 || b == 0; i++)
            {
                int idx = Convert.ToInt32(i % 5);
                string moveId = idx.ToString() + '-';
                startHeight = positions.Select(p => p.y).Max() + 4;
                List<Pos> r = rocks[idx].ToList();
                r = MoveRock(r, 2, startHeight);
                bool falling = true;
                while (falling)
                {
                    bool left = jets[jetIdx++ % jets.Length] == '<';
                    var rSide = MoveRock(r, left ? -1 : 1, 0);
                    bool validSide = ValidSidePosition(rSide, positions);
                    moveId += validSide ? (left ? "L" : "R") : "_";
                    if (validSide)
                        r = rSide;
                    var rDown = MoveRock(r, 0, -1);
                    if (ValidDownPosition(rDown, positions))
                    {
                        r = rDown;
                        moveId += "D";
                    }
                    else
                    {
                        falling = false;
                        foreach (Pos p in r)
                            positions.Add(p);
                        movements.Add(moveId);
                        heights.Add(positions.Select(p => p.y).Max() + 1);
                        if (b == 0)
                        {
                            List<int> mps = movementPositions.ContainsKey(moveId) ? movementPositions[moveId] : new();
                            mps.Add(i);
                            movementPositions[moveId] = mps;
                            int cycleLen = FindPatternCycle(movements, movementPositions, 100);
                            if (cycleLen > 0)
                            {
                                int idx0 = i;
                                int idx1 = i - cycleLen;
                                long repeats = (iMax - i) / cycleLen + 1;
                                long ib = i + repeats * cycleLen;
                                int deltaH = heights[idx0] - heights[idx1];
                                b = heights[idx0] + repeats * deltaH;
                                int goBack = Convert.ToInt32(ib - iMax + 1);
                                int goBackH = heights[idx0] - heights[idx0 - goBack];
                                b -= goBackH;
                            }
                        }
                    }
                }
            }
            int a = heights[2021];
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
