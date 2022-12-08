using AdventOfCode;
using System.Reflection;
using Node = AdventOfCode.Tree.Node<(string name, long size)>;

namespace aoc
{
    public class Day07
    {
        // No Space Left On Device: Traverse filesystem, calculate sizes
        public static long SumChildrenIfLessThan100000(Node node, long sum)
        {
            foreach (var c in node.children)
                sum = SumChildrenIfLessThan100000(c, sum);
            long totalSize = SumIncludingChildren(node, 0);
            if (totalSize <= 100000)
                sum += totalSize;
            return sum;
        }
        public static long SumIncludingChildren(Node node, long sum)
        {
            foreach (var c in node.children)
                sum = SumIncludingChildren(c, sum);
            sum += node.t.size;
            return sum;
        }
        public static void AddSizes(Node node, List<long> sizes)
        {
            foreach (var c in node.children)
                AddSizes(c, sizes);
            sizes.Add(SumIncludingChildren(node, 0));
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Node node = new Node(("/", 0), null);
            Node current = node;
            foreach (var s in input)
            {
                var v = s.Split(' ');
                if (v[0] == "$")
                {
                    var cmd = v[1];
                    if (cmd == "cd")
                    {
                        if (v[2] == "..")
                        {
                            current = current.parent!;
                        }
                        else
                        {
                            foreach (var c in current.children)
                            {
                                if (c.t.name == v[2])
                                {
                                    current = c;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (v[0] == "dir")
                {
                    // Check if exists before?
                    Node dir = new Node((v[1], 0), current);
                    current.children.Add(dir);
                }
                else // file
                {
                    var val = current.t;
                    val.size += long.Parse(s.Split(' ')[0]);
                    current.t = val;
                }
            }
            long a = SumChildrenIfLessThan100000(node, 0);
            List<long> sizes = new();
            AddSizes(node, sizes);
            sizes.Sort();
            long b = -1;
            long free = 70000000 - SumIncludingChildren(node, 0);
            foreach (var s in sizes)
            {
                if (free + s >= 30000000)
                {
                    b = s;
                    break;
                }
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
