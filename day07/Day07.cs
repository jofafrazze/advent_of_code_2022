using AdventOfCode;
using System.Reflection;
using Node = AdventOfCode.Tree.Node<(string name, int ownSize, int totalSize)>;

namespace aoc
{
    public class Day07
    {
        // No Space Left On Device: Traverse filesystem, calculate sizes
        public static void UpdateTotalSizes(Node node)
        {
            foreach (var c in node.children)
                UpdateTotalSizes(c);
            node.t.totalSize = node.t.ownSize + node.children.Select(c => c.t.totalSize).Sum();
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            Node root = new(("/", 0, 0), null);
            Node current = root;
            foreach (var s in ReadInput.Strings(Day, file))
            {
                var v = s.Split(' ');
                if (v[0] == "$")
                {
                    if (v[1] == "cd" && v[2] == "..")
                        current = current.parent!;
                    else if (v[1] == "cd" && v[2] != "/")
                        current = current.children.ToDictionary(x => x.t.name, x => x)[v[2]];
                }
                else if (v[0] == "dir")
                    current.children.Add(new Node((v[1], 0, 0), current));
                else // file
                    current.t.ownSize += int.Parse(v[0]);
            }
            UpdateTotalSizes(root);
            var sizes = root.ToList().Select(x => x.t.totalSize).OrderBy(n => n);
            int free = 70000000 - root.t.totalSize;
            return (sizes.Where(x => x <= 100000).Sum(), sizes.First(x => free + x >= 30000000));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
