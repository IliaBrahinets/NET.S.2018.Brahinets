using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class PolynomialTests
{
    public static TestContext TestContext { get; set; }

    [TestCase(new[] { 1.0, 4.0, 6.0 })]
    [TestCase(new[] { 0, 0, 1.0, 4.0, 6.0, 0, 0, 0 })]
    public void IndependentOfOrder(double[] Coeffs)
    {
        Polynomial a = new Polynomial(Coeffs);
        Polynomial b = new Polynomial(Coeffs.Reverse().ToArray(), Polynomial.CoeffsOrder.DecreasingOrder);

        Assert.True(a == b);
    }

    
    public static IEnumerable<TestCaseData> AddMethodTestData
    {
        get
        {
            yield return new TestCaseData(new Polynomial(new[] { 1d, 2, 3, 4 }),
                                          new Polynomial(new[] { 5d, 6, 7, 8 }))
                                 .Returns(new Polynomial(new[] { 6d, 8, 10, 12 }));

            yield return new TestCaseData(new Polynomial(new[] { 0, 0, 3.5, 4 }),
                                          new Polynomial(new[] { 1d, 0, 7, -1 }))
                                 .Returns(new Polynomial(new[] { 1d, 0, 10.5, 3 }));
        }
    }

    [Test, TestCaseSource(nameof(AddMethodTestData))]
    public Polynomial AddMethod(Polynomial a, Polynomial b)
    {
        Polynomial actual = a + b;

        return actual;
    }

    public static IEnumerable<TestCaseData> SubstractMethodTestData
    {
        get
        {
            yield return new TestCaseData(new Polynomial(new[] { 1d, 2, 3, 4 }),
                                          new Polynomial(new[] { 5d, 6, 7, 8 }))
                                 .Returns(new Polynomial(new[] { -4d, -4, -4, -4 }));

            yield return new TestCaseData(new Polynomial(new[] { 0d, 0, 3.5, 4 }),
                                          new Polynomial(new[] { 1d, 0, 7, -1 }))
                                 .Returns(new Polynomial(new[] { -1d, 0, -3.5, 5 }));
        }
    }

    [Test, TestCaseSource(nameof(SubstractMethodTestData))]
    public Polynomial SubstractMethod(Polynomial a, Polynomial b)
    {
        Polynomial actual = a - b;

        return actual;
    }

    public static IEnumerable<TestCaseData> MultiplyMethodTestData
    {
        get
        {
            yield return new TestCaseData(new Polynomial(new[] { 1d, 2, 3, 4 }),
                                          new Polynomial(new[] { 5d, 6, 7, 8 }))
                                 .Returns(new Polynomial(new[] { 5d, 16, 34, 60, 61, 52, 32 }));

            yield return new TestCaseData(new Polynomial(new[] { 0d, 0, 3.5, 4 }),
                                          new Polynomial(new[] { -1d, 0, -3.5, 5 }))
                                 .Returns(new Polynomial(new[] { 0d, 0, -3.5, -4, -12.25, 3.5, 20 }));
        }
    }

    [Test, TestCaseSource(nameof(MultiplyMethodTestData))]
    public Polynomial MultiplyMethod(Polynomial a, Polynomial b)
    {
        Polynomial actual = a * b;

        return actual;
    }

    public static IEnumerable<TestCaseData> MultiplyMethodOnDoubleTestData
    {
        get
        {
            yield return new TestCaseData(new Polynomial(new[] { 1d, 2, 3, 4 }), 2)
                                 .Returns(new Polynomial(new[] { 2d, 4, 6, 8 }));

            yield return new TestCaseData(new Polynomial(new[] { 0d, 0, 3.5, 4 }), 5)
                                 .Returns(new Polynomial(new[] { 0d, 0, 17.5, 20 }));
        }
    }

    [Test, TestCaseSource(nameof(MultiplyMethodOnDoubleTestData))]
    public Polynomial MultiplyMethod(Polynomial a, double b)
    {
        Polynomial actual = a * b;

        return actual;
    }

    public static IEnumerable<TestCaseData> EqualsTestData
    {
        get
        {
            yield return new TestCaseData(new Polynomial(new[] { 1.12d, 2.24, 3.78, 4.97 }),
                                          new Polynomial(new[] { 1.16d, 2.22, 3.74, 4.95 }))
                                 .Returns(true);
        }
    }

    [Test, TestCaseSource(nameof(EqualsTestData))]
    public bool EqualsMethod(Polynomial a, Polynomial b)
    {
        bool actual = a.Equals(b);

        return actual;
    }
    
    
}

