using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class MathExtensionTests
{

    public static IEnumerable<TestCaseData> TestDataOfTwoArguments
    {
        get
        {
            yield return new TestCaseData(10, 5).Returns(5);
            yield return new TestCaseData(18, 60).Returns(6);
            yield return new TestCaseData(2, 7).Returns(1);
            yield return new TestCaseData(-18, 60).Returns(6);
            yield return new TestCaseData(0, 100).Returns(100);
            yield return new TestCaseData(7, 11).Returns(1);
            yield return new TestCaseData(-2, -2).Returns(2);
            yield return new TestCaseData(0, 0).Returns(0);
            yield return new TestCaseData(int.MinValue, -10).Returns(2);
        }
    }

    [Test,TestCaseSource("TestDataOfTwoArguments")]
    public int GcdEuclidMethodTwoArgs(int a, int b)
    {
        return MathExtension.GcdByEuclid(a, b);
    }

    [Test, TestCaseSource("TestDataOfTwoArguments")]
    public int GcdBySteinMethodTwoArgs(int a, int b)
    {
        return MathExtension.GcdByStein(a, b);
    }

    public static IEnumerable<TestCaseData> TestDataOfThreeArguments
    {
        get
        {
            yield return new TestCaseData(10, 5, 6).Returns(1);
            yield return new TestCaseData(18, 60, 20).Returns(2);
            yield return new TestCaseData(2, 7, 1).Returns(1);
            yield return new TestCaseData(-18, 60, -60).Returns(6);
            yield return new TestCaseData(0, 100, 50).Returns(50);
            yield return new TestCaseData(7, 11, 2).Returns(1);
            yield return new TestCaseData(int.MinValue, -10, -2).Returns(2);
        }
    }

    [Test, TestCaseSource("TestDataOfThreeArguments")]
    public int GcdEuclidMethodThreeArgs(int a, int b, int c)
    {
        return MathExtension.GcdByEuclid(a, b, c);
    }

    [Test, TestCaseSource("TestDataOfThreeArguments")]
    public int GcdBySteinMethodThreeArgs(int a, int b, int c)
    {
        return MathExtension.GcdByStein(a, b, c);
    }
     
    public static IEnumerable<TestCaseData> TestDataOfManyArguments
    {
        get
        {
            yield return new TestCaseData(new int[] { 10, 5, 7, 5, 4, 1 }).Returns(1);
            yield return new TestCaseData(new int[] { 18, 60, 18, 60 }).Returns(6);
            yield return new TestCaseData(new int[] { 2, 7, 11, 6 }).Returns(1);
            yield return new TestCaseData(new int[] { -18, 60 }).Returns(6);
            yield return new TestCaseData(new int[] { 0, 100, 50 }).Returns(50);
            yield return new TestCaseData(new int[] { 22, 11, 77 }).Returns(11);
            yield return new TestCaseData(new int[] { int.MinValue, -10, -2 }).Returns(2);
        }
    }

    [Test, TestCaseSource("TestDataOfManyArguments")]
    public int GcdEuclidMethodManyArgs(int[] numbers)
    {
        return MathExtension.GcdByEuclid(numbers);
    }

    [Test, TestCaseSource("TestDataOfManyArguments")]
    public int GcdBySteinMethodManyArgs(int[] numbers)
    {
        return MathExtension.GcdByStein(numbers);
    }

    [Test]
    public void GcdByEuclidMethod_IntDotMinValuesPassed_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => MathExtension.GcdByEuclid(int.MinValue, int.MinValue));
    }

    [Test]
    public void GcdByEuclidMethod_IntDotMinValuesAndZeroPassed_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => MathExtension.GcdByEuclid(int.MinValue, 0));
    }
}

