using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class MathExtensionTests
{
    private static TestContext _testContext;

    public static TestContext TestContext
    {
        get
        {
            return _testContext;
        }
    }

    [TestCase(1, 5u, 0.0001, 1)]
    [TestCase(8, 3u, 0.0001, 2)]
    [TestCase(0.001, 3u, 0.0001, 0.1)]
    [TestCase(0.04100625, 4u, 0.0001, 0.45)]
    [TestCase(8, 3u, 0.0001, 2)]
    [TestCase(0.0279936, 7u, 0.0001, 0.6)]
    [TestCase(0.0081, 4u, 0.1, 0.3)]
    [TestCase(-0.008, 3u, 0.1, -0.2)]
    [TestCase(0.004241979, 9u, 0.00000001, 0.545)]
    [TestCase(2, 2u, 0.000001, 1.414213)]
    public void FindNthRootMethod(double number, uint n, double precision, double expectedResult)
    {
        double actual = MathExtension.FindNthRoot(number, n, precision);

        Assert.AreEqual(expectedResult, actual, precision);
    }

    [Test]
    public void FindNthRootMethod_8_15_minus0dot6_ArgumentOutOfRangeException()
    {
        double number = 8;
        uint n = 15;
        double precision = -0.6;

        Assert.Throws<ArgumentOutOfRangeException>(() => MathExtension.FindNthRoot(number, n, precision));
    }
    
    [Test]
    public void FindNthRootMethod_8_15_minus7_ArgumentOutOfRangeException()
    {
        double number = 8;
        uint n = 15;
        double precision = -7;

        Assert.Throws<ArgumentOutOfRangeException>(() => MathExtension.FindNthRoot(number, n, precision));
    }

    [Test]
    public void FindNthRootMethod_minus4_2_1Eminus4_ArgumentException()
    {
        double number = -4;
        uint n = 2;
        double precision = 1E-4;

        Assert.Throws<ArgumentException>(() => MathExtension.FindNthRoot(number, n, precision));
    }

    [TestCase(12u, ExpectedResult = 21)]
    [TestCase(513u, ExpectedResult = 531)]
    [TestCase(2017u, ExpectedResult = 2071)]
    [TestCase(414u, ExpectedResult = 441)]
    [TestCase(144u, ExpectedResult = 414)]
    [TestCase(1234321u, ExpectedResult = 1241233)]
    [TestCase(1234126u, ExpectedResult = 1234162)]
    [TestCase(3456432u, ExpectedResult = 3462345)]
    [TestCase(10u, ExpectedResult = null)]
    [TestCase(20u, ExpectedResult = null)]
    [TestCase(1987654321u, ExpectedResult = 2113456789)]
    [TestCase(0u, ExpectedResult = null)]
    [TestCase(UInt32.MaxValue,ExpectedResult = null)]
    public uint? FindNextBiggerNumber(uint number)
    {
        long executionTime = 0;

        uint? actual = MathExtension.FindNextBiggerNumber(number, out executionTime);

        TestContext.WriteLine($"ElapsedTicks: { executionTime }");

        return actual;
    }
}
