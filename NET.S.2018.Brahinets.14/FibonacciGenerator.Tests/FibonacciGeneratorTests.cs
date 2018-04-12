using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class FibonacciGeneratorTests
{
    [Test]
    [TestCase(2, ExpectedResult = new long[] { 1, 1 })]
    [TestCase(4, ExpectedResult = new long[] { 1, 1, 2, 3 })]
    [TestCase(8, ExpectedResult = new long[] { 1, 1, 2, 3, 5, 8, 13, 21 })]
    public long[] GetSeriesMethod(int n)
    {
        return FibonacciGenerator.GetSeries(n);
    }

    [Test]
    public void GetSeriesMethod_TooMuchElem_OverflowException()
    {
        int n = 10000;

        Assert.Throws<OverflowException>(() => FibonacciGenerator.GetSeries(n));
    }

    [Test]
    public void GetSeriesMethod_TooMuchElem_ArgumentOutOfRangeException()
    {
        int n = -1;

        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciGenerator.GetSeries(n));
    }
}

