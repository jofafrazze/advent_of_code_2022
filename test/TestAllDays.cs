using Xunit;

namespace test
{
    public class Input
    {
        public static readonly string actual = "input.txt";
        public static readonly string example = "example.txt";
    }

    public class TestDay01
    {
        [Fact]
        public static void Example() => Assert.Equal((24000, 45000), aoc.Day01.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((70698, 206643), aoc.Day01.DoPuzzle(Input.actual));
    }

    public class TestDay02
    {
        [Fact]
        public static void Example() => Assert.Equal((15, 12), aoc.Day02.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((13526, 14204), aoc.Day02.DoPuzzle(Input.actual));
    }

    public class TestDay03
    {
        [Fact]
        public static void Example() => Assert.Equal((157, 70), aoc.Day03.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((8401, 2641), aoc.Day03.DoPuzzle(Input.actual));
    }
    public class TestDay04
    {
        [Fact]
        public static void Example() => Assert.Equal((2, 4), aoc.Day04.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((536, 845), aoc.Day04.DoPuzzle(Input.actual));
    }
    public class TestDay05
    {
        [Fact]
        public static void Example() => Assert.Equal(("CMZ", "MCD"), aoc.Day05.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal(("ZBDRNPMVH", "WDLPFNNNB"), aoc.Day05.DoPuzzle(Input.actual));
    }
    public class TestDay06
    {
        [Fact]
        public static void Example() => Assert.Equal((7, 19), aoc.Day06.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((1702, 3559), aoc.Day06.DoPuzzle(Input.actual));
    }
    public class TestDay07
    {
        [Fact]
        public static void Example() => Assert.Equal((95437, 24933642), aoc.Day07.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((1770595, 2195372), aoc.Day07.DoPuzzle(Input.actual));
    }
    public class TestDay08
    {
        [Fact]
        public static void Example() => Assert.Equal((21, 8), aoc.Day08.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((1807, 480000), aoc.Day08.DoPuzzle(Input.actual));
    }
    public class TestDay09
    {
        [Fact]
        public static void Example() => Assert.Equal((13, 1), aoc.Day09.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((5683, 2372), aoc.Day09.DoPuzzle(Input.actual));
    }
    public class TestDay10
    {
        readonly static string se =
            "##  ##  ##  ##  ##  ##  ##  ##  ##  ##  " + System.Environment.NewLine +
            "###   ###   ###   ###   ###   ###   ### " + System.Environment.NewLine +
            "####    ####    ####    ####    ####    " + System.Environment.NewLine +
            "#####     #####     #####     #####     " + System.Environment.NewLine +
            "######      ######      ######      ####" + System.Environment.NewLine +
            "#######       #######       #######     " + System.Environment.NewLine;
        [Fact]
        public static void Example() => Assert.Equal((13140, se), aoc.Day10.DoPuzzle(Input.example));
        readonly static string sa =
            "###   ##  ###  #  # ###  ####  ##  ###  " + System.Environment.NewLine +
            "#  # #  # #  # #  # #  # #    #  # #  # " + System.Environment.NewLine +
            "#  # #    #  # #### ###  ###  #  # ###  " + System.Environment.NewLine +
            "###  # ## ###  #  # #  # #    #### #  # " + System.Environment.NewLine +
            "#    #  # #    #  # #  # #    #  # #  # " + System.Environment.NewLine +
            "#     ### #    #  # ###  #### #  # ###  " + System.Environment.NewLine;
        [Fact]
        public static void Test() => Assert.Equal((13520, sa), aoc.Day10.DoPuzzle(Input.actual));
    }
    public class TestDay11
    {
        [Fact]
        public static void Example() => Assert.Equal((10605L, 2713310158L), aoc.Day11.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((57348L, 14106266886L), aoc.Day11.DoPuzzle(Input.actual));
    }
    public class TestDay12
    {
        [Fact]
        public static void Example() => Assert.Equal((31, 29), aoc.Day12.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((350, 349), aoc.Day12.DoPuzzle(Input.actual));
    }
    public class TestDay13
    {
        [Fact]
        public static void Example() => Assert.Equal((13, 140), aoc.Day13.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((6415, 20056), aoc.Day13.DoPuzzle(Input.actual));
    }
    public class TestDay14
    {
        [Fact]
        public static void Example() => Assert.Equal((24, 93), aoc.Day14.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((808, 26625), aoc.Day14.DoPuzzle(Input.actual));
    }
    public class TestDay15
    {
        [Fact]
        public static void Example() => Assert.Equal((26, 56000011L), aoc.Day15.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((4861076, 10649103160102), aoc.Day15.DoPuzzle(Input.actual));
    }
    public class TestDay16
    {
        [Fact]
        public static void Example() => Assert.Equal((1651, 1707), aoc.Day16.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((1754, 2474), aoc.Day16.DoPuzzle(Input.actual));
    }
    public class TestDay17
    {
        [Fact]
        public static void Example() => Assert.Equal((3068, 1514285714288), aoc.Day17.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((3193, 1577650429835), aoc.Day17.DoPuzzle(Input.actual));
    }
    public class TestDay18
    {
        [Fact]
        public static void Example() => Assert.Equal((64, 58), aoc.Day18.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((3586, 2072), aoc.Day18.DoPuzzle(Input.actual));
    }
    public class TestDay19
    {
        [Fact]
        public static void Example() => Assert.Equal((33, 3472), aoc.Day19.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((790, 7350), aoc.Day19.DoPuzzle(Input.actual));
    }
    public class TestDay20
    {
        [Fact]
        public static void Example() => Assert.Equal((3L, 1623178306L), aoc.Day20.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((8372L, 7865110481723), aoc.Day20.DoPuzzle(Input.actual));
    }
    public class TestDay21
    {
        [Fact]
        public static void Example() => Assert.Equal((152L, 301L), aoc.Day21.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((84244467642604, 3759569926192), aoc.Day21.DoPuzzle(Input.actual));
    }
    public class TestDay22
    {
        [Fact]
        public static void Example() => Assert.Equal((6032, 5031), aoc.Day22.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((80392, 19534), aoc.Day22.DoPuzzle(Input.actual));
    }
    public class TestDay23
    {
        [Fact]
        public static void Example() => Assert.Equal((110, 20), aoc.Day23.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((3947, 1012), aoc.Day23.DoPuzzle(Input.actual));
    }
    public class TestDay24
    {
        [Fact]
        public static void Example() => Assert.Equal((18, 54), aoc.Day24.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((230, 713), aoc.Day24.DoPuzzle(Input.actual));
    }
    public class TestDay25
    {
        [Fact]
        public static void Example() => Assert.Equal(("2=-1=0", 0), aoc.Day25.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal(("20=022=21--=2--12=-2", 0), aoc.Day25.DoPuzzle(Input.actual));
    }
}
