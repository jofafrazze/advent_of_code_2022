using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day11
    {
        // Today: 
        class Monkey
        {
            public List<long> items;
            public Func<long, long> operation;
            public Func<long, bool> test;
            public int trueNext, falseNext;
            public long nInspected;
            public Monkey(List<long> i, Func<long, long> o, Func<long, bool> t, int tn, int fn) 
                { items = i; operation = o; test = t; trueNext = tn; falseNext = fn; nInspected = 0; }
        };
        //static readonly List<Monkey> monkeys = new()
        //{
        //    new Monkey(new() { 79, 98 }, delegate (long w) { return w * 19; }, delegate (long d) { return d % 23 == 0; }, 2, 3),
        //    new Monkey(new() { 54, 65, 75, 74 }, delegate (long w) { return w + 6; }, delegate (long d) { return d % 19 == 0; }, 2, 0),
        //    new Monkey(new() { 79, 60, 97 }, delegate (long w) { return w * w; }, delegate (long d) { return d % 13 == 0; }, 1, 3),
        //    new Monkey(new() { 74 }, delegate (long w) { return w + 3; }, delegate (long d) { return d % 17 == 0; }, 0, 1),
        //};
        static readonly List<Monkey> monkeysA = new()
        {
            new Monkey(new() { 91, 58, 52, 69, 95, 54 }, delegate (long w) { return w * 13; }, delegate (long d) { return d % 7 == 0; }, 1, 5),
            new Monkey(new() { 80, 80, 97, 84 }, delegate (long w) { return w * w; }, delegate (long d) { return d % 3 == 0; }, 3, 5),
            new Monkey(new() { 86, 92, 71 }, delegate (long w) { return w + 7; }, delegate (long d) { return d % 2 == 0; }, 0, 4),
            new Monkey(new() { 96, 90, 99, 76, 79, 85, 98, 61 }, delegate (long w) { return w + 4; }, delegate (long d) { return d % 11 == 0; }, 7, 6),
            new Monkey(new() { 60, 83, 68, 64, 73 }, delegate (long w) { return w * 19; }, delegate (long d) { return d % 17 == 0; }, 1, 0),
            new Monkey(new() { 96, 52, 52, 94, 76, 51, 57 }, delegate (long w) { return w + 3; }, delegate (long d) { return d % 5 == 0; }, 7, 3),
            new Monkey(new() { 75 }, delegate (long w) { return w + 5; }, delegate (long d) { return d % 13 == 0; }, 4, 2),
            new Monkey(new() { 83, 75 }, delegate (long w) { return w + 1; }, delegate (long d) { return d % 19 == 0; }, 2, 6),
        };
        static readonly List<Monkey> monkeysB = new()
        {
            new Monkey(new() { 91, 58, 52, 69, 95, 54 }, delegate (long w) { return w * 13; }, delegate (long d) { return d % 7 == 0; }, 1, 5),
            new Monkey(new() { 80, 80, 97, 84 }, delegate (long w) { return w * w; }, delegate (long d) { return d % 3 == 0; }, 3, 5),
            new Monkey(new() { 86, 92, 71 }, delegate (long w) { return w + 7; }, delegate (long d) { return d % 2 == 0; }, 0, 4),
            new Monkey(new() { 96, 90, 99, 76, 79, 85, 98, 61 }, delegate (long w) { return w + 4; }, delegate (long d) { return d % 11 == 0; }, 7, 6),
            new Monkey(new() { 60, 83, 68, 64, 73 }, delegate (long w) { return w * 19; }, delegate (long d) { return d % 17 == 0; }, 1, 0),
            new Monkey(new() { 96, 52, 52, 94, 76, 51, 57 }, delegate (long w) { return w + 3; }, delegate (long d) { return d % 5 == 0; }, 7, 3),
            new Monkey(new() { 75 }, delegate (long w) { return w + 5; }, delegate (long d) { return d % 13 == 0; }, 4, 2),
            new Monkey(new() { 83, 75 }, delegate (long w) { return w + 1; }, delegate (long d) { return d % 19 == 0; }, 2, 6),
        };
        static long DoMonkeyBusiness(List<Monkey> monkeys, bool partA)
        {
            for (long i = 0; i < (partA ? 20 : 10000); i++)
            {
                foreach (var m in monkeys)
                {
                    while (m.items.Count > 0)
                    {
                        long w = m.items[0];
                        m.items.RemoveAt(0);
                        w = m.operation(w);
                        if (partA)
                            w /= 3;
                        else
                            w %= 7 * 3 * 2 * 11 * 17 * 5 * 13 * 19;
                        if (m.test(w))
                            monkeys[m.trueNext].items.Add(w);
                        else
                            monkeys[m.falseNext].items.Add(w);
                        m.nInspected++;
                    }
                }
            }
            var n = monkeys.Select(m => m.nInspected).OrderByDescending(x => x).ToList();
            return n[0] * n[1];
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (DoMonkeyBusiness(monkeysA, true), DoMonkeyBusiness(monkeysB, false));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
