using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day21
    {
        // Monkey Math: Traverse tree to calculate, then mess up with unknown in leaf
        static long Calc(string k, Dictionary<string, (bool bx, string g, string op, string h)> dict)
        {
            var v = dict[k];
            if (v.op == "")
                return int.Parse(v.g);
            else
            {
                long a = Calc(v.g, dict);
                long b = Calc(v.h, dict);
                v = dict[k];
                bool ba = v.g == "humn";
                bool bb = v.h == "humn";
                bool bx = v.bx || ba || bb || dict[v.g].bx || dict[v.h].bx;
                //string sa = ba ? "humn" : a.ToString();
                //string sb = bb ? "humn" : b.ToString();
                if (bx)
                    dict[k] = (bx, v.g, v.op, v.h);
                if (v.op == "+")
                    return a + b;
                else if (v.op == "-")
                    return a - b;
                else if (v.op == "*")
                    return a * b;
                else if (v.op == "/")
                    return a / b;
                else
                    throw new Exception();
            }
        }
        static long GoDown(string k, Dictionary<string, (bool bx, string g, string op, string h)> dict, long res)
        {
            var v = dict[k];
            {
                bool gHasX = dict[v.g].bx || v.g == "humn";
                string kX = gHasX ? v.g : v.h;
                string kNum = gHasX ? v.h : v.g;
                long num = Calc(kNum, dict);
                bool haveHumn = v.g == "humn" || v.h == "humn";
                if (haveHumn)
                {
                    if (gHasX)
                    {
                        if (v.op == "+")
                            return res - num;
                        else if (v.op == "-")
                            return res + num;
                        else if (v.op == "*")
                            return res / num;
                        else if (v.op == "/")
                            return res * num;
                        else
                            throw new Exception();
                    }
                    else
                    {
                        if (v.op == "+")
                            return res - num;
                        else if (v.op == "-")
                            return num - res;
                        else if (v.op == "*")
                            return res / num;
                        else if (v.op == "/")
                            return num / res;
                        else
                            throw new Exception();
                    }
                }
                if (gHasX)
                {
                    if (v.op == "+")
                        return GoDown(kX, dict, res - num);
                    else if (v.op == "-")
                        return GoDown(kX, dict, res + num);
                    else if (v.op == "*")
                        return GoDown(kX, dict, res / num);
                    else if (v.op == "/")
                        return GoDown(kX, dict, num * res);
                    else
                        throw new Exception();
                }
                else
                {
                    if (v.op == "+")
                        return GoDown(kX, dict, res - num);
                    else if (v.op == "-")
                        return GoDown(kX, dict, num - res);
                    else if (v.op == "*")
                        return GoDown(kX, dict, res / num);
                    else if (v.op == "/")
                        return GoDown(kX, dict, num / res);
                    else
                        throw new Exception();
                }
            }
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var strings = ReadInput.Strings(Day, file);
            Dictionary<string,(bool bx, string g, string op, string h)> dict = new();
            foreach (var s in strings)
            {
                var v = s.Split(' ');
                var op = v.Length > 2 ? v[2] : "";
                var h = v.Length > 2 ? v[3] : "";
                dict[v[0].Substring(0, v[0].Length - 1)] = (false, v[1], op, h);
            }
            long a = Calc("root", dict);
            bool gHasX = dict[dict["root"].g].bx;
            string kX = gHasX ? dict["root"].g : dict["root"].h;
            string kNum = gHasX ? dict["root"].h : dict["root"].g;
            long b = GoDown(kX, dict, Calc(kNum, dict));
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
