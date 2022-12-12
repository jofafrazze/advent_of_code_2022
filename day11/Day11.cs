using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day11
    {
        // Monkey in the Middle: Parse, play game, guess b worry suppression mechanism
        record class Monkey
        {
            public Queue<long> items;
            public int opMult, opAdd, mod, trueNext, falseNext, nInspected;
            public Monkey(Queue<long> i, int om, int oa, int m, int tn, int fn) 
                { items = i; opMult = om; opAdd = oa; mod = m; trueNext = tn; falseNext = fn; nInspected = 0; }
        };
        static long DoMonkeyBusiness(List<Monkey> monkeys, bool partA)
        {
            long bmod = monkeys.Select(m => m.mod).Aggregate(1, (a, b) => a * b);
            for (int i = 0; i < (partA ? 20 : 10000); i++)
                foreach (var m in monkeys)
                    while (m.items.Count > 0)
                    {
                        long w = m.items.Dequeue();
                        w *= m.opMult < 0 ? w : m.opMult;
                        w += m.opAdd;
                        w = partA ? w / 3 : w % bmod;
                        monkeys[w % m.mod == 0 ? m.trueNext : m.falseNext].items.Enqueue(w);
                        m.nInspected++;
                    }
            var n = monkeys.Select(m => (long)m.nInspected).OrderByDescending(x => x).ToList();
            return n[0] * n[1];
        }
        static List<Monkey> Monkeys(List<List<string>> groups)
        {
            List<Monkey> monkeys = new();
            foreach (var g in groups)
            {
                var items = new Queue<long>(Extract.Longs(g[1]));
                var v = g[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int n = v[5][0] == 'o' ? -1 : int.Parse(v[5]);
                int om = v[4] == "*" ? n : 1;
                int oa = v[4] == "+" ? n : 0;
                int mod = int.Parse(g[3].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last());
                int tm = g[4].Last() - '0';
                int fm = g[5].Last() - '0';
                monkeys.Add(new(items, om, oa, mod, tm, fm));
            }
            return monkeys;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var g = ReadInput.StringGroups(Day, file);
            return (DoMonkeyBusiness(Monkeys(g), true), DoMonkeyBusiness(Monkeys(g), false));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
