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
    }
    public class TestDay11
    {
    }
    public class TestDay12
    {
    }
    public class TestDay13
    {
    }
    public class TestDay14
    {
    }
    public class TestDay15
    {
    }
    public class TestDay16
    {
    }
    public class TestDay17
    {
    }
    public class TestDay18
    {
    }
    public class TestDay19
    {
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
