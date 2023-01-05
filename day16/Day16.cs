using AdventOfCode;
using System.Linq;
using System.Reflection;
using System.Text;

namespace aoc
{
    public class Day16
    {
        // Proboscidea Volcanium: 
        struct NameTunnel { public string valve; public int cost; public NameTunnel(string v, int c) { valve = v; cost = c; } }
        struct IdTunnel { public int valve; public int cost; public IdTunnel(int v, int c) { valve = v; cost = c; } }
        static List<string> valveNamesList = new();
        static Dictionary<string, int> valveNameToIdx = new();
        struct State : IEquatable<State>
        {
            public int idx;     // Valve idx, 0 indexed
            public int time;    // minutes left in state
            public int open;    // Bits to indicate which valves are open
            public State(int i, int t, int o) { idx = i; time = t; open = o; }
            public override bool Equals(object? obj) => obj != null && obj is State other && Equals(other);
            public bool Equals(State s) => idx == s.idx && time == s.time && open == s.open;
            public override int GetHashCode() => HashCode.Combine(idx, time, open);
        }
        static List<NameTunnel> GetEffectiveTunnels(string valveName, Dictionary<string, (int rate, List<NameTunnel> tunnels)> dict)
        {
            var tunnels = dict[valveName].tunnels.ToList();
            List<NameTunnel> done = tunnels.Where(z => dict[z.valve].rate > 0 || z.valve == "AA").ToList();
            List<NameTunnel> todo = tunnels.Except(done).ToList();
            HashSet<string> visited = new() { valveName };
            while (todo.Any())
            {
                NameTunnel t = todo[0];
                visited.Add(t.valve);
                todo.RemoveAt(0);
                List<NameTunnel> step = dict[t.valve].tunnels.Select(z => new NameTunnel(z.valve, t.cost + 1)).ToList();
                foreach (NameTunnel t2 in step)
                    if (dict[t2.valve].rate == 0 && t2.valve != "AA")
                    {
                        if (!visited.Contains(t2.valve))
                            todo.Add(new NameTunnel(t2.valve, t2.cost));
                    }
                    else
                        if (!done.Where(z => z.valve == t2.valve).Any() && t2.valve != valveName)
                            done.Add(t2);
            }
            return done;
        }
        static (int, Dictionary<State, int>, int) GetMaxPressure(List<string> input, int minutes, bool tryAll)
        {
            var fullDict = new Dictionary<string, (int rate, List<NameTunnel> tunnels)>();
            foreach (var s in input)
            {
                var v = s.Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int n = Extract.Ints(s)[0];
                List<NameTunnel> list = new();
                for (int i = 9; i < v.Length; i++)
                    list.Add(new NameTunnel(v[i], 1));
                fullDict[v[1]] = (n, list);
            }
            // Only keep "AA" and valves with rate > 0
            var nameDict = new Dictionary<string, (int rate, List<NameTunnel> tunnels)>();
            foreach (var k in fullDict.Keys)
                if (fullDict[k].rate > 0 || k == "AA")
                    nameDict[k] = (fullDict[k].rate, GetEffectiveTunnels(k, fullDict));
            int nValves = nameDict.Count;
            valveNamesList = nameDict.Keys.OrderBy(k => k).ToList();
            valveNameToIdx = valveNamesList.ToDictionary(w => w, w => valveNamesList.IndexOf(w));
            int allIdValvesMask = 0;
            for (int i = 0; i < valveNamesList.Count; i++)
                allIdValvesMask |= 1 << i;
            var idxDict = new Dictionary<int, (int rate, List<IdTunnel> tunnels, int minCost)>();
            foreach (var (k, v) in nameDict)
            {
                var tunnels = v.tunnels.Select(w => new IdTunnel(valveNameToIdx[w.valve], w.cost)).ToList();
                idxDict[valveNameToIdx[k]] = (v.rate, tunnels, tunnels.Select(w => w.cost).Min());
            }
            int[] idxRates = idxDict.OrderBy(w => w.Key).Select(w => w.Value.rate).ToArray();
            int[] idxMinCosts = idxDict.OrderBy(w => w.Key).Select(w => w.Value.minCost).ToArray();
            var pressure = new Dictionary<State, int>();
            var toTest = new Queue<State>();
            State state0 = new(valveNameToIdx["AA"], minutes, 1);
            pressure[state0] = 0;
            toTest.Enqueue(state0);
            int maxPressure = 0;
            while (toTest.TryDequeue(out State cur))
            {
                if (cur.time <= 0)
                    continue;
                List<(State, int)> next = new();
                // Open valve?
                if ((cur.open & (1 << cur.idx)) == 0)
                   next.Add((new(cur.idx, cur.time - 1, cur.open | (1 << cur.idx)), pressure[cur] + idxDict[cur.idx].rate * (cur.time - 1)));
                // Just move on?
                if ((cur.open & allIdValvesMask) != allIdValvesMask)
                    foreach (var to in idxDict[cur.idx].tunnels)
                        if (cur.time > to.cost)
                            next.Add((new(to.valve, cur.time - to.cost, cur.open), pressure[cur]));
                foreach (var (s, p) in next)
                {
                    int bestFuturePressure = tryAll ? int.MaxValue : p;
                    for (int i = 1; i < nValves && !tryAll; i++)
                        if ((s.open & (1 << i)) == 0)
                            bestFuturePressure += idxRates[i] * (s.time - idxMinCosts[s.idx]);
                    if ((!pressure.ContainsKey(s) || pressure[s] < p) && bestFuturePressure > maxPressure)
                    {
                        toTest.Enqueue(s);
                        pressure[s] = p;
                        if (p > maxPressure)
                            maxPressure = p;
                    }
                }
            }
            //Console.WriteLine("Tested {0} states in total.", pressure.Count);
            return (maxPressure, pressure, allIdValvesMask);
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var (a, _, _) = GetMaxPressure(input, 30, false);
            var (_, pressure, allMask) = GetMaxPressure(input, 26, true);
            Dictionary<int, int> op = new();
            foreach (var p in pressure)
            {
                int open = p.Key.open & (allMask - 1);
                op[open] = op.ContainsKey(open) ? Math.Max(p.Value, op[open]) : p.Value;
            }
            int b = 0;
            var opList = op.OrderByDescending(s => s.Value).ToList();
            for (int i = 0; i < opList.Count; i++)
            { 
                var me = opList[i];
                if (me.Value < b / 2)
                    break;
                for (int j = i + 1; j < opList.Count; j++)
                {
                    var elephant = opList[j];
                    if ((me.Key & elephant.Key) == 0)
                    {
                        int p = me.Value + elephant.Value;
                        if (p > b)
                            b = p;
                        else
                            break;
                    }
                }
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, false);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
