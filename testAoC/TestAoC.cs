using System.Collections.Generic;
using System.Linq;
using Xunit;
using Ir = AdventOfCode.IntRange;

namespace testAoC
{
    public class IntRangeEquality
    {
        [Theory]
        [InlineData(0, 1, 0, 1, true)]
        [InlineData(-1, 1, -1, 1, true)]
        [InlineData(-1, 1, 0, 1, false)]
        [InlineData(10, 10, 10, 10, true)]
        [InlineData(-9, 9, -9, 8, false)]
        [InlineData(-3, -3, -3, -3, true)]
        [InlineData(-3, -2, -3, -3, false)]
        public void Test(int s1, int e1, int s2, int e2, bool eq)
        {
            Ir r1 = new(s1, e1);
            Ir r2 = new(s2, e2);
            if (eq)
                Assert.Equal(r1, r2);
            else
                Assert.NotEqual(r1, r2);
        }
    }
    public class IntRangeComparison
    {
        [Theory]
        [InlineData(0, 1, 0, 1, 0)]
        [InlineData(-1, 1, -1, 1, 0)]
        [InlineData(-1, 1, 0, 1, -1)]
        [InlineData(10, 10, 10, 10, 0)]
        [InlineData(-9, 9, -9, 8, 1)]
        [InlineData(-3, -3, -3, -3, 0)]
        [InlineData(-3, -2, -3, -3, 1)]
        [InlineData(int.MinValue, int.MinValue, int.MaxValue - 1, int.MaxValue, -1)]
        public void Test(int s1, int e1, int s2, int e2, int relation)
        {
            Ir r1 = new(s1, e1);
            Ir r2 = new(s2, e2);
            if (relation < 0)
                Assert.True(r1 < r2);
            else if (relation > 0)
                Assert.True(r1 > r2);
            else
                Assert.True(r1 == r2);
        }
    }
    public class IntRangeUnion1
    {
        [Theory]
        [InlineData(0, 1, 0, 1, 0, 1)]
        [InlineData(0, 3, 0, 1, 0, 3)]
        [InlineData(0, 9, 4, 11, 0, 11)]
        [InlineData(0, 1, -2, 0, -2, 1)]
        [InlineData(-6, 123, -12, -6, -12, 123)]
        public void Test(int s1, int e1, int s2, int e2, int s, int e)
        {
            Ir r1 = new(s1, e1);
            Ir r2 = new(s2, e2);
            var rt = r1.Union(r2);
            var r = new List<Ir>() { new(s, e) };
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class IntRangeUnion2
    {
        [Theory]
        [InlineData(0, 1, 3, 4, 0, 1, 3, 4)]
        [InlineData(3, 4, 0, 1, 0, 1, 3, 4)]
        public void Test(int s1, int e1, int s2, int e2, int s, int e, int sb, int eb)
        {
            Ir r1 = new(s1, e1);
            Ir r2 = new(s2, e2);
            var rt = r1.Union(r2);
            var r = new List<Ir>() { new(s, e), new(sb, eb) };
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class IntRangeIntersection1
    {
        [Theory]
        [InlineData(0, 1, 0, 1, 0, 1)]
        [InlineData(0, 10, 6, 111, 6, 10)]
        [InlineData(6, 111, 0, 10, 6, 10)]
        [InlineData(9, 10, 6, 111, 9, 10)]
        [InlineData(6, 111, 9, 10, 9, 10)]
        public void Test(int s1, int e1, int s2, int e2, int s, int e)
        {
            Ir r1 = new(s1, e1);
            Ir r2 = new(s2, e2);
            var rt = r1.Intersect(r2);
            var r = new List<Ir>() { new(s, e) };
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class IntRangeIntersection0
    {
        [Theory]
        [InlineData(0, 1, 2, 3)]
        [InlineData(2, 3, 0, 1)]
        public void Test(int s1, int e1, int s2, int e2)
        {
            Ir r1 = new(s1, e1);
            Ir r2 = new(s2, e2);
            var rt = r1.Intersect(r2);
            var r = new List<Ir>();
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class IntRangeExcept0
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(-77, -77, -77)]
        public void Test(int s1, int e1, int i)
        {
            Ir r1 = new(s1, e1);
            var rt = r1.Except(i);
            var r = new List<Ir>();
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class IntRangeExcept1
    {
        [Theory]
        [InlineData(0, 1, 1, 0, 0)]
        [InlineData(0, 10, 0, 1, 10)]
        [InlineData(-111, 111, 111, -111, 110)]
        [InlineData(0, 10, -33, 0, 10)]
        [InlineData(0, 10, 33, 0, 10)]
        public void Test(int s1, int e1, int i, int s, int e)
        {
            Ir r1 = new(s1, e1);
            var rt = r1.Except(i);
            var r = new List<Ir>() { new(s, e) };
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class IntRangeExcept2
    {
        [Theory]
        [InlineData(0, 10, 1, 0, 0, 2, 10)]
        [InlineData(111, 1234, 1000, 111, 999, 1001, 1234)]
        public void Test(int s1, int e1, int i, int s, int e, int sb, int eb)
        {
            Ir r1 = new(s1, e1);
            var rt = r1.Except(i);
            var r = new List<Ir>() { new(s, e), new(sb, eb) };
            Assert.True(rt.SequenceEqual(r));
        }
    }
    public class TestStaticUnion
    {
        [Fact]
        public void Test1()
        {
            var r0 = new List<Ir>() { new(0, 1), new(2, 3), new(4, 5), new(6, 7) };
            var r = Ir.NormalizeUnion(r0);
            Assert.True(r.SequenceEqual(r0));
        }
        [Fact]
        public void Test2()
        {
            var r0 = new List<Ir>() { new(2, 3), new(0, 10), new(4, 5), new(-10, -2) };
            var result = new List<Ir>() { new(-10, -2), new(0, 10) };
            var r = Ir.NormalizeUnion(r0);
            Assert.True(r.SequenceEqual(result));
        }
    }
}