namespace aoc
{
    public class TimeAllDays
    {
        static List<Action> days = new()
        {
            test.TestDay01.Test,
            test.TestDay02.Test,
            test.TestDay03.Test,
            test.TestDay04.Test,
            test.TestDay05.Test,
            test.TestDay06.Test,
            test.TestDay07.Test,
            test.TestDay08.Test,
            test.TestDay09.Test,
            test.TestDay10.Test,
            test.TestDay11.Test,
            test.TestDay12.Test,
            test.TestDay13.Test,
            test.TestDay14.Test,
            test.TestDay15.Test,
            test.TestDay16.Test,
            test.TestDay17.Test,
            test.TestDay18.Test,
            test.TestDay19.Test,
            test.TestDay20.Test,
            test.TestDay21.Test,
            test.TestDay22.Test,
            test.TestDay23.Test,
            test.TestDay24.Test,
            test.TestDay25.Test,
        };
        public static bool IsDebug =>
#if DEBUG
                true;
#else
                false;
#endif
        static void WriteToTextWriter(TextWriter tw, Action<string> fnWrite, string str)
        {
            Console.SetOut(tw);
            fnWrite(str);
        }
        static void Log(StreamWriter log, string str)
        {
            var tmp = Console.Out;
            WriteToTextWriter(log, Console.Write, str);
            WriteToTextWriter(tmp, Console.Write, str);
        }
        static void LogLine(StreamWriter log, string str)
        {
            var tmp = Console.Out;
            WriteToTextWriter(log, Console.WriteLine, str);
            WriteToTextWriter(tmp, Console.WriteLine, str);
        }
        static void Main()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
            string buildConf = IsDebug ? "Debug" : "Release";
            string now = DateTime.Now.ToString("yyyy-M-dd_HH-mm-ss");
            StreamWriter log = new(dir + "\\log\\TimeAoC2022_" + buildConf + "_" + now + ".txt");
            LogLine(log, $"Running all {days.Count} days in " + buildConf + " configuration: ");
            List<(int day, int ms)> stats = new();
            for (int i = 1; i <= days.Count; i++)
            {
                Log(log, $"{i},");
                var w = System.Diagnostics.Stopwatch.StartNew();
                days[i - 1]();
                w.Stop();
                stats.Add((i, (int)w.ElapsedMilliseconds));
            }
            LogLine(log, "done.");
            stats = stats.OrderBy(w => w.ms).ToList();
            foreach (var (day, ms) in stats)
                LogLine(log, $"Day {day,2:00}: {ms,5} ms");
            log.Close();
        }
    }
}
