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
        public void ExampleA() => Assert.Equal(157, aoc.Day03.PartA(Input.example));
        [Fact]
        public void TestA() => Assert.Equal(8401, aoc.Day03.PartA(Input.actual));
        [Fact]
        public void ExampleB() => Assert.Equal(70, aoc.Day03.PartB(Input.example));
        [Fact]
        public void TestB() => Assert.Equal(2641, aoc.Day03.PartB(Input.actual));
    }
}