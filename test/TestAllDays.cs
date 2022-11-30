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
        public void ExampleA() => Assert.Equal(7, aoc.Day01.PartA(Input.example));
        [Fact]
        public void TestA() => Assert.Equal(1301, aoc.Day01.PartA(Input.actual));
        [Fact]
        public void ExampleB() => Assert.Equal(5, aoc.Day01.PartB(Input.example));
        [Fact]
        public void TestB() => Assert.Equal(1346, aoc.Day01.PartB(Input.actual));
    }
}