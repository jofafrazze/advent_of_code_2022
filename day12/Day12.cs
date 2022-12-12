using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day12
    {
        // Hill Climbing Algorithm: Traverse map, first up then down
        public static int DoPuzzleA(string file)
        {
            var m = Map.Build(ReadInput.Strings(Day, file));
            Pos sPos = new Pos(), ePos = new Pos();
            foreach (Pos p in m.Positions())
            {
                if (m[p] == 'S')
                    sPos = p;
                if (m[p] == 'E')
                    ePos = p;
            }
            var steps = new Dictionary<Pos, int>();
            steps[sPos] = 0;
            var next = new Queue<Pos>();
            next.Enqueue(sPos);
            while (next.Any())
            {
                Pos p = next.Dequeue();
                //Console.WriteLine("Best so far: {0}, {1} = {2}", p.x, p.y, steps[p]);
                foreach (var n in CoordsRC.Neighbours4(p))
                {
                    if (m.HasPosition(n))
                    {
                        int nextVal = m[n] == 'E' ? 'z' : m[n];
                        int d = nextVal - m[p];
                        if (m[p] == 'S' || (m[n] != 'S' && d <= 1))
                        {
                            if (!steps.ContainsKey(n) || steps[n] > steps[p] + 1)
                            {
                                steps[n] = steps[p] + 1;
                                if (m[n] != 'E')
                                    next.Enqueue(n);
                            }
                        }
                    }
                }
            }
            return steps[ePos];
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var m = Map.Build(ReadInput.Strings(Day, file));
            Pos sPos = new Pos(), ePos = new Pos();
            foreach (Pos p in m.Positions())
            {
                if (m[p] == 'S')
                    sPos = p;
                if (m[p] == 'E')
                    ePos = p;
            }
            m[sPos] = 'a';
            m[ePos] = 'z';
            var steps = new Dictionary<Pos, int>();
            steps[ePos] = 0;
            var next = new Queue<Pos>();
            next.Enqueue(ePos);
            while (next.Any())
            {
                Pos p = next.Dequeue();
                //Console.WriteLine("Best so far: {0}, {1} = {2}", p.y, p.x, steps[p]);
                foreach (var n in CoordsRC.Neighbours4(p))
                {
                    if (m.HasPosition(n))
                    {
                        int d = m[n] - m[p];
                        if (d >= -1)
                        {
                            if (!steps.ContainsKey(n) || steps[n] > steps[p] + 1)
                            {
                                steps[n] = steps[p] + 1;
                                next.Enqueue(n);
                            }
                        }
                    }
                }
            }
            int a = DoPuzzleA(file);
            int b = steps.Where(z => m[z.Key] == 'a').Select(z => z.Value).Min();
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
