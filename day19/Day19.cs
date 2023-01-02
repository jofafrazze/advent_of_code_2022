using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day19
    {
        // Not Enough Minerals: Try too many states
        // a = ore
        // b = clay
        // c = obsidian
        // d = geode
        struct State : IEquatable<State>
        {
            public byte t;
            public byte ca; // Currencies [a-d]
            public byte cb;
            public byte cc;
            public byte cd;
            public byte ra; // Robots [a-d]
            public byte rb;
            public byte rc;
            public byte rd;
            public bool cba; // Could build last time but did not [a-c]
            public bool cbb;
            public bool cbc;
            public State(byte t_,
                byte a_, byte b_, byte c_, byte d_,
                byte ra_, byte rb_, byte rc_, byte rd_,
                bool cba_, bool cbb_, bool cbc_)
            { t = t_; 
                ca = a_; cb = b_; cc = c_; cd = d_;
                ra = ra_; rb = rb_; rc = rc_; rd = rd_;
                cba = cba_; cbb = cbb_; cbc = cbc_;
            }
            public override bool Equals(object? obj) => obj != null && obj is State other && Equals(other);
            public bool Equals(State s) => t == s.t &&
                ca == s.ca && cb == s.cb && cc == s.cc && cd == s.cd &&
                ra == s.ra && rb == s.rb && rc == s.rc && rd == s.rd;
            public override int GetHashCode() => HashCode.Combine(t, ca << 24 | cb << 16 | cc << 8 | cd, ra << 24 | rb << 16 | rc << 8 | rd);
        }
        record struct Blueprint(int ra_ca, int rb_ca, int rc_ca, int rc_cb, int rd_ca, int rd_cc, int maxa, int maxb, int maxc);
        static int GetOptimisticMaxGeodes(State s, int rd_cc, int rc_cb, int minutes)
        {
            for (int i = s.t; i < minutes; i++)
            {
                State next = s;
                if (s.cc >= rd_cc)
                {
                    s.cc = (byte)(s.cc - rd_cc);
                    next.rd++;
                }
                if (s.cb >= rc_cb)
                {
                    s.cb = (byte)(s.cb - rc_cb);
                    next.rc++;
                }
                next.rb++;
                s.cb += s.rb;
                s.cc += s.rc;
                s.cd += s.rd;
                s.rb = next.rb;
                s.rc = next.rc;
                s.rd = next.rd;
            }
            return s.cd;
        }
        static (int, int) GetMaxGeodes(Blueprint bp, int minutes)
        {
            State s0 = new(0, 0, 0, 0, 0, 1, 0, 0, 0, false, false, false);
            int max0 = GetOptimisticMaxGeodes(s0, bp.rd_cc, bp.rc_cb, minutes);
            Dictionary<State, int> optimisticMax = new() { { s0, max0 } };
            var toTry = new PriorityQueue<State, int>(new (State, int)[] { (s0, max0) });
            int maxGeodes = 0;
            while (toTry.TryDequeue(out State cur, out int _))
            {
                if (cur.cd > maxGeodes)
                    maxGeodes = cur.cd;
                else if (optimisticMax[cur] <= maxGeodes)
                    continue;
                if (cur.t >= minutes)
                    continue;
                List<State> nextStates = new(4);
                cur.t++;
                if (cur.ca >= bp.rd_ca && cur.cc >= bp.rd_cc)
                {
                    // If we can built new d robot, don't bother with the other possible states
                    State s = cur;
                    s.ca = (byte)(s.ca - bp.rd_ca);
                    s.cc = (byte)(s.cc - bp.rd_cc);
                    s.rd += 1;
                    nextStates.Add(s);
                }
                else
                {
                    // We never need more currencies than maxa a, maxb b and maxc c
                    bool cCanBuild = cur.ca >= bp.rc_ca && cur.cb >= bp.rc_cb && cur.rc < bp.maxc;
                    bool bCanBuild = cur.ca >= bp.rb_ca && cur.rb < bp.maxb;
                    bool aCanBuild = cur.ca >= bp.ra_ca && cur.ra < bp.maxa;
                    State templ = cur;
                    templ.cba = false;
                    templ.cbb = false;
                    templ.cbc = false;
                    if (cCanBuild && !cur.cbc)
                    {
                        State s = templ;
                        s.ca = (byte)(s.ca - bp.rc_ca);
                        s.cb = (byte)(s.cb - bp.rc_cb);
                        s.rc++;
                        nextStates.Add(s);
                    }
                    if (bCanBuild && !cur.cbb)
                    {
                        State s = templ;
                        s.ca = (byte)(s.ca - bp.rb_ca);
                        s.rb++;
                        nextStates.Add(s);
                    }
                    if (aCanBuild && !cur.cba)
                    {
                        State s = templ;
                        s.ca = (byte)(s.ca - bp.ra_ca);
                        s.ra++;
                        nextStates.Add(s);
                    }
                    if (cur.rc < bp.maxc && cur.rb < bp.maxb && cur.ra < bp.maxa)
                    {
                        cur.cba = aCanBuild;
                        cur.cbb = bCanBuild;
                        cur.cbc = cCanBuild;
                        nextStates.Add(cur);
                    }
                }
                foreach (var ss in nextStates)
                {
                    var s = ss;
                    s.ca += cur.ra;
                    s.cb += cur.rb;
                    s.cc += cur.rc;
                    s.cd += cur.rd;
                    if (!optimisticMax.ContainsKey(s))
                    {
                        int omg = GetOptimisticMaxGeodes(s, bp.rd_cc, bp.rc_cb, minutes);
                        optimisticMax[s] = omg;
                        if (omg > maxGeodes)
                            toTry.Enqueue(s, s.rd * 50 * 1000 + s.t * 1000 + omg);
                    }
                }
            }
            return (maxGeodes, optimisticMax.Count);
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<Blueprint> blueprints = new();
            foreach (var s in input)
            {
                var v = Extract.Ints(s);
                int maxa = new List<int>() { v[1], v[2], v[3], v[5] }.Max();
                blueprints.Add(new(v[1], v[2], v[3], v[4], v[5], v[6], maxa, v[4], v[6]));
            }
            int a = 0;
            for (int i = 0; i < blueprints.Count; i++)
            {
                var (maxGeodes, nVisited) = GetMaxGeodes(blueprints[i], 24);
                int aAdd = (i + 1) * maxGeodes;
                //Console.WriteLine("Checked {0} states (a add = {1})", nVisited, aAdd);
                a += aAdd;
            }
            int b = 1;
            int nPart2Blueprints = Math.Min(3, blueprints.Count);
            for (int i = 0; i < nPart2Blueprints; i++)
            {
                var (maxGeodes, nVisited) = GetMaxGeodes(blueprints[i], 32);
                //Console.WriteLine("Checked {0} states (b mult = {1})", nVisited, maxGeodes);
                b *= maxGeodes;
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
