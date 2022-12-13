using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day13
    {
        // Distress Signal: Can you parse it? No you can't!
        static List<string> GetListElements(string s)
        {
            List<string> elements = new List<string>();
            int depth = 0;
            int startIdx = 1;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '[')
                    depth++;
                else if (c == ']')
                    depth--;
                if ((c == ',' && depth == 1) || (c == ']' && depth == 0))
                {
                    elements.Add(s.Substring(startIdx, i - startIdx));
                    startIdx = i + 1;
                }
            }
            return elements;
        }
        static string ListToString(List<string> l)
        {
            string s = "[";
            for (int i = 0; i < l.Count; i++)
            {
                s += l[i];
                if (i < l.Count - 1)
                    s += ",";
            }
            return s + "]";
        }
        static string AssureList(string s)
        {
            if (s.Length == 0 || s[0] != '[')
                return "[" + s + "]";
            return s;
        }
        static int CompareElements(string ls, string rs)
        {
            var left = GetListElements(AssureList(ls));
            var right = GetListElements(AssureList(rs));
            if (left.Count == 0 && right.Count == 0)
                return 0;
            else if (left.Count == 0 && right.Count > 0)
                return -1;
            else if (left.Count > 0 && right.Count == 0)
                return 1;
            else
            {
                bool leftEmpty = left[0].Length == 0;
                bool rightEmpty = right[0].Length == 0;
                if (leftEmpty && rightEmpty)
                    return 0;
                else if (leftEmpty && !rightEmpty)
                    return -1;
                else if (!leftEmpty && rightEmpty)
                    return 1;
                int cmp;
                if (left[0][0] != '[' && right[0][0] != '[')
                {
                    cmp = int.Parse(left[0]).CompareTo(int.Parse(right[0]));
                    if (cmp != 0)
                        return cmp;
                }
                else
                {
                    string l = AssureList(left[0]);
                    string r = AssureList(right[0]);
                    cmp = CompareElements(l, r);
                    if (cmp != 0)
                        return cmp;
                }
                left.RemoveAt(0);
                right.RemoveAt(0);
                return CompareElements(ListToString(left), ListToString(right));
            }
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            int i = 1;
            int sum = 0;
            foreach (var g in input)
            {
                if (CompareElements(g[0], g[1]) < 0)
                    sum += i;
                i++;
            }
            var strings = ReadInput.Strings(Day, file).Where(s => s.Length > 0).ToList();
            strings.Add("[[2]]");
            strings.Add("[[6]]");
            strings.Sort(delegate (string s1, string s2) { return CompareElements(s1, s2); });
            int idx1 = -1, idx2 = -1;
            for (int n = 0; n < strings.Count && (idx1 < 0 || idx2 < 0); n++)
            {
                if (strings[n] == "[[2]]")
                    idx1 = n + 1;
                if (strings[n] == "[[6]]")
                    idx2 = n + 1;
            }
            return (sum, idx1 * idx2);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
