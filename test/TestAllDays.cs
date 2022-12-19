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
        public void Example() => Assert.Equal((24000, 45000), aoc.Day01.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((70698, 206643), aoc.Day01.DoPuzzle(Input.actual));
    }

    public class TestDay02
    {
        [Fact]
        public void Example() => Assert.Equal((15, 12), aoc.Day02.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((13526, 14204), aoc.Day02.DoPuzzle(Input.actual));
    }

    public class TestDay03
    {
        [Fact]
        public void Example() => Assert.Equal((157, 70), aoc.Day03.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((8401, 2641), aoc.Day03.DoPuzzle(Input.actual));
    }
    public class TestDay04
    {
        [Fact]
        public void Example() => Assert.Equal((2, 4), aoc.Day04.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((536, 845), aoc.Day04.DoPuzzle(Input.actual));
    }
    public class TestDay05
    {
        [Fact]
        public void Example() => Assert.Equal(("CMZ", "MCD"), aoc.Day05.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal(("ZBDRNPMVH", "WDLPFNNNB"), aoc.Day05.DoPuzzle(Input.actual));
    }
    public class TestDay06
    {
        [Fact]
        public void Example() => Assert.Equal((7, 19), aoc.Day06.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((1702, 3559), aoc.Day06.DoPuzzle(Input.actual));
    }
    public class TestDay07
    {
        [Fact]
        public void Example() => Assert.Equal((95437, 24933642), aoc.Day07.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((1770595, 2195372), aoc.Day07.DoPuzzle(Input.actual));
    }
    public class TestDay08
    {
        [Fact]
        public void Example() => Assert.Equal((21, 8), aoc.Day08.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((1807, 480000), aoc.Day08.DoPuzzle(Input.actual));
    }
    public class TestDay09
    {
        [Fact]
        public void Example() => Assert.Equal((13, 1), aoc.Day09.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((5683, 2372), aoc.Day09.DoPuzzle(Input.actual));
    }
    public class TestDay10
    {
        readonly string se =
            "##  ##  ##  ##  ##  ##  ##  ##  ##  ##  " + System.Environment.NewLine +
            "###   ###   ###   ###   ###   ###   ### " + System.Environment.NewLine +
            "####    ####    ####    ####    ####    " + System.Environment.NewLine +
            "#####     #####     #####     #####     " + System.Environment.NewLine +
            "######      ######      ######      ####" + System.Environment.NewLine +
            "#######       #######       #######     " + System.Environment.NewLine;
        [Fact]
        public void Example() => Assert.Equal((13140, se), aoc.Day10.DoPuzzle(Input.example));
        readonly string sa =
            "###   ##  ###  #  # ###  ####  ##  ###  " + System.Environment.NewLine +
            "#  # #  # #  # #  # #  # #    #  # #  # " + System.Environment.NewLine +
            "#  # #    #  # #### ###  ###  #  # ###  " + System.Environment.NewLine +
            "###  # ## ###  #  # #  # #    #### #  # " + System.Environment.NewLine +
            "#    #  # #    #  # #  # #    #  # #  # " + System.Environment.NewLine +
            "#     ### #    #  # ###  #### #  # ###  " + System.Environment.NewLine;
        [Fact]
        public void Test() => Assert.Equal((13520, sa), aoc.Day10.DoPuzzle(Input.actual));
    }
    public class TestDay11
    {
        [Fact]
        public void Example() => Assert.Equal((10605L, 2713310158L), aoc.Day11.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((57348L, 14106266886L), aoc.Day11.DoPuzzle(Input.actual));
    }
    public class TestDay12
    {
        [Fact]
        public void Example() => Assert.Equal((31, 29), aoc.Day12.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((350, 349), aoc.Day12.DoPuzzle(Input.actual));
    }
    public class TestDay13
    {
        [Fact]
        public void Example() => Assert.Equal((13, 140), aoc.Day13.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((6415, 20056), aoc.Day13.DoPuzzle(Input.actual));
    }
    public class TestDay14
    {
        [Fact]
        public void Example() => Assert.Equal((24, 93), aoc.Day14.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((808, 26625), aoc.Day14.DoPuzzle(Input.actual));
    }
    public class TestDay15
    {
        //[Fact]
        //public void Example() => Assert.Equal((26, 56000011L), aoc.Day15.DoPuzzle(Input.example));
        //[Fact]
        //public void Test() => Assert.Equal((4861076, 10649103160102), aoc.Day15.DoPuzzle(Input.actual));
    }
    public class TestDay16
    {
    }
    public class TestDay17
    {
        [Fact]
        public void Example() => Assert.Equal((3068, 1514285714288), aoc.Day17.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((3193, 1577650429835), aoc.Day17.DoPuzzle(Input.actual));
    }
    public class TestDay18
    {
        [Fact]
        public void Example() => Assert.Equal((64, 58), aoc.Day18.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((3586, 2072), aoc.Day18.DoPuzzle(Input.actual));
    }
    public class TestDay19
    {
        [Fact]
        public void Example() => Assert.Equal((33, 3472), aoc.Day19.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((790, 7350), aoc.Day19.DoPuzzle(Input.actual));
    }
    public class TestDay20
    {
    }
    public class TestDay21
    {
    }
    public class TestDay22
    {
    }
    public class TestDay23
    {
    }
    public class TestDay24
    {
    }
    public class TestDay25
    {
    }
}
