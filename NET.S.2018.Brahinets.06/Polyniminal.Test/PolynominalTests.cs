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

    [TestCase(new[] {1.0,4.0,6.0}, new[] { 6.0, 5.5, 2.3 })]
    public void AddMethod(double[] CoeffsIn, double[] CoeffsOut)
    {
        double[] Coeffs = { 3.5,0,0 };

        Polynominal a = new Polynominal(Coeffs);
        Polynominal b = new Polynominal(Coeffs);

        Polynominal c = a * b;

        foreach (double item in a)
        {
            TestContext.WriteLine(a.ToString());
        }

        Assert.True(a.Equals(b));
    }
}

