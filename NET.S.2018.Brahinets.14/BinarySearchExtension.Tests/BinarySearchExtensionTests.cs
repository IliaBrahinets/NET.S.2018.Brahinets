using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class BinarySearchExtensionTests
{
    [Test]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 9, 10 }, 4, ExpectedResult = 3)]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 9, 10 }, 2, ExpectedResult = 1)]
    [TestCase(new int[] { 1 }, 1, ExpectedResult = 0)]
    [TestCase(new int[] { }, 0, ExpectedResult = -1)]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 9, 10 }, 20, ExpectedResult = -1)]
    public int BinarySearchMethod(int[] array, int item)
    {
        return array.BinarySearch(item);
    }

    [Test]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 9, 10 }, 2, 0, 3, ExpectedResult = 1)]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 9, 10 }, 3, 0, 5, ExpectedResult = 2)]
    public int BinarySearchMethod(int[] array, int item, int left, int right)
    {
        return array.BinarySearch(item, left, right, null);
    }

    private class WithoutIComparable {}

    [Test]
    public void BinarySearchMethod_TypeWithoutIComparableAndComparerAsNull_InvalidOperationException
        ()
    {
        WithoutIComparable[] array = new WithoutIComparable[1];

        Assert.Throws<InvalidOperationException>(() => array.BinarySearch(new WithoutIComparable(), null));
    }

    [Test]
    public void BinarySearchMethod_ArrayIsNull_ArgumentException
        ()
    {
        int[] array = new int[1];
        int left = -1;
        int right = 0;

        Assert.Throws<ArgumentException>(() => array.BinarySearch(1, left, right, null));
    }

    [Test]
    public void BinarySearchMethod_ArrayIsNull_ArgumentNullEception
        ()
    {
        int[] array = null;

        Assert.Throws<ArgumentNullException>(() => array.BinarySearch(1, 0, 0, null));
    }

}

