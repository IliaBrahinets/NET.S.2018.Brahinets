using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class JaggedArraySortTests
{
    private static TestContext TestContext { get; set; }

    public static IEnumerable<TestCaseData> TestData
    {
        get
        {
            yield return new TestCaseData((object)new int[][]
            {
                new int[]{1,2,3,4,5},
                new int[]{-1,-2,4,5,4},
                new int[]{-1,-5,-4,-4,-3},
                new int[]{1,2,3,4,5},
                null
            });
        }
    }
    #region Using interfaces
    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsMaxElemsMethod(int[][] array)
    {
        IComparer<int[]> comparer = new ByRowsMaxElemsComparer();

        JaggedArraySort.Sort(array, comparer);
        
        Assert.True(IsSorted(array, comparer));
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsMinElemsMethod(int[][] array)
    {
        IComparer<int[]> comparer  = new ByRowsMinElemsComparer();

        JaggedArraySort.Sort(array, comparer);

        Assert.True(IsSorted(array, comparer));
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsSumMethod(int[][] array)
    {
        IComparer<int[]> comparer = new ByRowsSumComparer();

        JaggedArraySort.Sort(array, comparer);

        Assert.True(IsSorted(array, comparer));
    }

    private bool IsSorted(int[][] array, IComparer<int[]> comparer)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (comparer.Compare(array[i], array[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }
    #endregion

    #region Using delegates
    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsMaxElemsMethodByDelegates(int[][] array)
    {
        Comparison<int[]> comparer = SortCriteriasAsStaticMethods.ByRowsMax;

        JaggedArraySort.Sort(array, comparer);

        Assert.True(IsSorted(array, comparer));
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsMinElemsMethodByDelegates(int[][] array)
    {
        Comparison<int[]> comparer = SortCriteriasAsStaticMethods.ByRowsMin;

        JaggedArraySort.Sort(array, comparer);

        Assert.True(IsSorted(array, comparer));
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsSumMethodByDelegates(int[][] array)
    {
        Comparison<int[]> comparer = SortCriteriasAsStaticMethods.ByRowsSum;

        JaggedArraySort.Sort(array, comparer);

        Assert.True(IsSorted(array, comparer));
    }

    private bool IsSorted(int[][] array, Comparison<int[]> comparer)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (comparer(array[i], array[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }
    #endregion
}

