using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class JaggedArraySort
{
    /// <summary>
    /// Sorting the array by bubble sort.
    /// </summary>
    /// <param name="comparer">Determines a comparer is used when sorting.</param>
    /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
    public static void Sort(int[][] array, IComparer<int[]> comparer)
    {
        if(array == null)
        {
            throw new ArgumentNullException($"{nameof(array)} is null");
        }

        bool isAlreadySorted = true;

        int lastIndex = array.Length - 1;

        for (int j = lastIndex; j >= 0; j--)
        {
            for (int i = 0; i < j; i++)
            {
                if (comparer.Compare(array[i], array[i + 1]) > 0)
                {
                    Swap(ref array[i], ref array[i + 1]);
                    isAlreadySorted = false;
                }
            }

            if (isAlreadySorted == true)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Sorting the array by bubble sort.
    /// </summary>
    /// <param name="comparer">Determines a comparer using when sorting.</param>
    /// <exception cref="ArgumentNullException">Thrown when comparer or array in null.</exception>
    public static void Sort(int[][] array, Comparison<int[]> comparer)
    {
        if (comparer == null)
        {
            throw new ArgumentNullException($"{nameof(comparer)} is null");
        }

        var _comparer = Comparer<int[]>.Create(comparer);

        Sort(array, Comparer<int[]>.Create(comparer));
    }

    private static void Swap<T>(ref T A, ref T B)
    {
        T tmp = A;
        A = B;
        B = tmp;
    }
}

