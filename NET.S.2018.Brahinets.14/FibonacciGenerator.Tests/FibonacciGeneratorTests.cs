using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class FibonacciGeneratorTests
{

    private static IEnumerable<TestCaseData> TestData
    {
        get
        {
            yield return new TestCaseData(2).Returns(new BigInteger[] { 1, 1 });
            yield return new TestCaseData(4).Returns(new BigInteger[] { 1, 1, 2, 3 });
            yield return new TestCaseData(8).Returns(new BigInteger[] { 1, 1, 2, 3, 5, 8, 13, 21 });
        }
                
    }

    [Test]
    [TestCaseSource(nameof(TestData))]
    public BigInteger[] GetSeriesMethod(int n)
    {
        return FibonacciGenerator.GetSeries(n).ToArray();
    }

    [Test]
    public void GetSeriesMethod_TooMuchElem_ArgumentOutOfRangeException()
    {
        int n = -1;

        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciGenerator.GetSeries(n).ToArray());
    }
}

