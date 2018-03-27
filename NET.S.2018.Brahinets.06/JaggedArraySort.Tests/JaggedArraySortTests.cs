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

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsMaxElemsMethod(int[][] array)
    {
        JaggedArraySort.SortMode mode = new JaggedArraySort.SortByRowsMaxElems();

        JaggedArraySort.Sort(array, mode);
        
        Assert.True(IsSorted(array, mode));
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsMinElemsMethod(int[][] array)
    {
        JaggedArraySort.SortMode mode = new JaggedArraySort.SortByRowsMinElems();

        JaggedArraySort.Sort(array, mode);

        Assert.True(IsSorted(array, mode));
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void JaggedArraySortByRowsSumMethod(int[][] array)
    {
        JaggedArraySort.SortMode mode = new JaggedArraySort.SortByRowsSum();

        JaggedArraySort.Sort(array, mode);

        Assert.True(IsSorted(array, mode));
    }

    private bool IsSorted(int[][] array, JaggedArraySort.SortMode mode)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (mode.Compare(array[i], array[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }
}

