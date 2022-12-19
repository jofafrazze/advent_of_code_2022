using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day19
    {
        // Today: 
        struct State : IEquatable<State>
        {
            public int t;
            public int ca;
            public int cb;
            public int cc;
            public int cd;
            public int ra;
            public int rb;
            public int rc;
            public int rd;
            public State(int t_, 
                int a_, int b_, int c_, int d_,
                int ra_, int rb_, int rc_, int rd_
                )
            { t = t_; 
                ca = a_; cb = b_; cc = c_; cd = d_;
                ra = ra_; rb = rb_; rc = rc_; rd = rd_;
            }
            public override bool Equals(object? obj) => obj != null && obj is State other && Equals(other);
            public bool Equals(State s) => t == s.t &&
                ca == s.ca && cb == s.cb && cc == s.cc && cd == s.cd &&
                ra == s.ra && rb == s.rb && rc == s.rc && rd == s.rd;
            public override int GetHashCode() => HashCode.Combine(
                HashCode.Combine(t, ca, cb, cc, cd), HashCode.Combine(ra, rb, rc, rd));
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            // Cost in ore(=a), cost 2 in clay(=b)
            List<(int ra_ca, int rb_ca, int rc_ca, int rc_cb, int rd_ca, int rd_cc, int needa, int needb, int needc)> blueprints = new();
            foreach (var s in input)
            {
                var v = Extract.Ints(s);
                int needa = new List<int>() { v[1], v[2], v[3], v[5] }.Max();
                blueprints.Add((v[1], v[2], v[3], v[4], v[5], v[6], needa, v[4], v[6]));
            }
            int a = 0;
            int b = 1;
            int nPart2Blueprints = 3;
            //int minutePrinted;
            for (int bpIdx = 0; bpIdx < blueprints.Count; bpIdx++)
            {
                //minutePrinted = -1;
                int minutesA = 24;
                int minutesB = 32;
                int minutes = bpIdx < nPart2Blueprints ? minutesB : minutesA;
                State s0 = new(0, 0, 0, 0, 0, 1, 0, 0, 0);
                HashSet<State> visited = new() { s0 };
                Queue<State> toTry = new Queue<State>(new[] { s0 });
                //var toTry = new PriorityQueue<State, int>(new (State, int)[] { (s0, 0) });
                var bp = blueprints[bpIdx];
                int maxGeodesA = 0;
                int maxGeodes = 0;
                int maxGeodeRobots = 0;
                //while (toTry.TryDequeue(out State sD, out int geode))
                while (toTry.Any())
                {
                    State sD = toTry.Dequeue();
                    //if (sD.t > minutePrinted)
                    //{
                    //    Console.WriteLine("{0} states to try (this has t = {1}: {2} ore, {3} clay, {4} obsidian, {5} geodes [{6} ra, {7} rb, {8} rc, {9} rd])",
                    //        toTry.Count, sD.t, sD.ca, sD.cb, sD.cc, sD.cd, sD.ra, sD.rb, sD.rc, sD.rd);
                    //    minutePrinted = sD.t;
                    //}
                    List<State> nextStates = new();
                    State next = sD;
                    next.t = sD.t + 1;
                    if (next.t <= minutes)
                    {
                        if (next.ca >= bp.rd_ca && next.cc >= bp.rd_cc)
                        {
                            State s = next;
                            s.ca -= bp.rd_ca;
                            s.cc -= bp.rd_cc;
                            s.rd += 1;
                            nextStates.Add(s);
                        }
                        if (next.ca >= bp.rc_ca && next.cb >= bp.rc_cb && next.rc < bp.needc)
                        {
                            State s = next;
                            s.ca -= bp.rc_ca;
                            s.cb -= bp.rc_cb;
                            s.rc += 1;
                            nextStates.Add(s);
                        }
                        if (next.ca >= bp.rb_ca && next.ra < bp.needb)
                        {
                            State s = next;
                            s.ca -= bp.rb_ca;
                            s.rb += 1;
                            nextStates.Add(s);
                        }
                        if (next.ca >= bp.ra_ca && next.ra < bp.needa)
                        {
                            State s = next;
                            s.ca -= bp.ra_ca;
                            s.ra += 1;
                            nextStates.Add(s);
                        }
                        nextStates.Add(next);
                    }
                    foreach (var s in nextStates)
                    {
                        var ss = s;
                        ss.ca += next.ra;
                        ss.cb += next.rb;
                        ss.cc += next.rc;
                        ss.cd += next.rd;
                        if (!visited.Contains(ss) && ss.cd >= maxGeodes - 3 && ss.rc >= maxGeodeRobots - 3)
                        {
                            visited.Add(ss);
                            toTry.Enqueue(ss);
                        }
                        if (ss.cd > maxGeodes)
                            maxGeodes = ss.cd;
                        if (ss.rd > maxGeodeRobots)
                            maxGeodeRobots = ss.rd;
                        if (ss.cd > maxGeodesA && ss.t <= minutesA)
                            maxGeodesA = ss.cd;
                    }
                }
                int aAdd = (bpIdx + 1) * maxGeodesA;
                Console.WriteLine("Checked {0} states (a add = {1})", visited.Count, aAdd);
                a += aAdd;
                if (bpIdx < nPart2Blueprints)
                {
                    Console.WriteLine("(b factor = {0})", maxGeodes);
                    b *= maxGeodes;
                }
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
