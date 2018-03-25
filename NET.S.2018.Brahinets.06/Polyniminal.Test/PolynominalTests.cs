using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class PolynominalTests
{

    private static TestContext TestContext { get; set; }

    [Test]
    public void AddMethod()
    {
        double[] Coeffs = { 0, 0, 0, 1.0, 2.0, 3.5 };

        Polynominal a = new Polynominal(Coeffs);
        Polynominal b = new Polynominal(Coeffs);

        Polynominal c = a * b;

        foreach(double item in c)
        {
            TestContext.WriteLine(item);
        }

        CollectionAssert.AreEqual(a, c);
    }
}

