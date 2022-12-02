using AdventOfCode;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace aoc
{
    public class Day02
    {
        // Today: 

        public static Object PartA(string file)
        {
            var pointsOwn = new List<int>() { 1, 2, 3 };
            var pointsWin = new Dictionary<int, int>() { { -2, 0 }, { -1, 6 }, { 0, 3 }, { 1, 0 }, {2, 6} };
            var input = ReadInput.Strings(Day, file);
            int sum = 0;
            foreach (var s in input)
            {
                var s1 = s.Replace("A", "0").Replace("X", "0");
                var s2 = s1.Replace("B", "1").Replace("Y", "1");
                var s3 = s2.Replace("C", "2").Replace("Z", "2");
                var v = s3.Split(" ").Select(int.Parse).ToList();
                int delta = v[0] - v[1];
                int pOwn = pointsOwn[v[1]];
                int pWin = pointsWin[delta];
                sum += pOwn + pWin;
            }
            return sum;
        }

        public static Object PartB(string file)
        {
            var pointsOwn = new List<int>() { 1, 2, 3 };
            var pointsSelect = new List<int>() { -1, 0, 1 };
            var pointsEnd = new List<int>() { 0, 3, 6 };
            var input = ReadInput.Strings(Day, file);
            int sum = 0;
            foreach (var s in input)
            {
                var s1 = s.Replace("A", "0").Replace("X", "0");
                var s2 = s1.Replace("B", "1").Replace("Y", "1");
                var s3 = s2.Replace("C", "2").Replace("Z", "2");
                var v = s3.Split(" ").Select(int.Parse).ToList();
                int ownIdx = (v[0] + pointsSelect[v[1]] + 3) % 3;
                int pOwn = pointsOwn[ownIdx];
                int endIdx = v[1];
                int pEnd = pointsEnd[endIdx];
                sum += pOwn + pEnd;
            }
            return sum;
        }

        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
