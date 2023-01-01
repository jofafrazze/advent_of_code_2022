using AdventOfCode;
using System.Reflection;
using CLL = AdventOfCode.ExtendCollections;

namespace aoc
{
    public class Day20
    {
        // Grove Positioning System: rotate until dizzy, then use modulo

        delegate LinkedListNode<long> FnStep(LinkedListNode<long> a);
        delegate void FnAdd(LinkedListNode<long> b, LinkedListNode<long> c);
        static long MixList(List<long> input, int nMix, long k)
        {
            var list = new LinkedList<long>();
            var nodes = new List<LinkedListNode<long>>();
            for (int i = 0; i < input.Count; i++)
                nodes.Add(list.AddLast(input[i] * k));
            for (int m = 0; m < nMix; m++)
                for (int i = 0; i < nodes.Count; i++)
                {
                    var pos = nodes[i];
                    long nMove = pos.Value % (nodes.Count - 1);
                    if (nMove != 0)
                    {
                        FnStep step = (nMove > 0) ? new FnStep(CLL.NextOrFirst) : new FnStep(CLL.PreviousOrLast);
                        FnAdd listAdd = (nMove > 0) ? new(list.AddAfter) : new(list.AddBefore);
                        for (int _ = 0; _ < Math.Abs(nMove); _++)
                            pos = step(pos);
                        list.Remove(nodes[i]);
                        listAdd(pos, nodes[i]);
                    }
                }
            var n = nodes.First(w => w.Value == 0);
            return 
                n.NextOrFirst(1000).Value + 
                n.NextOrFirst(2000).Value + 
                n.NextOrFirst(3000).Value;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Longs(Day, file);
            long a = MixList(input, 1, 1);
            long b = MixList(input, 10, 811589153);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
