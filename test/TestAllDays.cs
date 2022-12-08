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
        public void Example() => Assert.Equal((95437L, 24933642L), aoc.Day07.DoPuzzle(Input.example));
        [Fact]
        public void Test() => Assert.Equal((1770595L, 2195372L), aoc.Day07.DoPuzzle(Input.actual));
    }
    public class TestDay08
    {
        [Fact]
        public void ExampleA() => Assert.Equal(21, aoc.Day08.PartA(Input.example));
        [Fact]
        public void TestA() => Assert.Equal(1807, aoc.Day08.PartA(Input.actual));
        [Fact]
        public void ExampleB() => Assert.Equal(8, aoc.Day08.PartB(Input.example));
        [Fact]
        public void TestB() => Assert.Equal(480000, aoc.Day08.PartB(Input.actual));
    }
}

