using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class BinarySearchExtension
{
    /// <summary>
    /// Performs searching the item in the array by the binary search algorithm.
    /// As a comparer the default comparer is used.
    /// </summary>
    /// <returns>Return index of the item or -1 if the can't find the given item.</returns>
    public static int BinarySearch<T>(this IList<T> array, T item)
    {
        DataValidation(array, null, null, null);

        IComparer<T> comparer = Comparer<T>.Default;

        int left = 0;
        int right = array.Count - 1;

        return BinarySearch(array, left, right, item, comparer);
    }

    /// <summary>
    /// Performs searching the item in the array by the binary search algorithm.
    /// As a comparer the given comparer is used.
    /// </summary>
    /// <returns>Return index of the item or -1 if the can't find the given item.</returns>
    public static int BinarySearch<T>(this IList<T> array, T item, IComparer<T> comparer)
    {
        DataValidation(array, null, null, comparer);

        comparer = comparer ?? Comparer<T>.Default;

        int left = 0;
        int right = array.Count - 1;

        return BinarySearch(array, left, right, item, comparer);
    }

    /// <summary>
    /// Performs searching the item in the array from the left to the right index by the binary search algorithm.
    /// As a comparer the given comparer is used.
    /// </summary>
    /// <returns>Return index of the item or -1 if the can't find the given item.</returns>
    public static int BinarySearch<T>(this IList<T> array, T item, int left, int right, IComparer<T> comparer)
    {
        DataValidation(array, left, right, comparer);

        comparer = comparer ?? Comparer<T>.Default;

        return BinarySearch(array, left, right, item, comparer);

    }

    private static int BinarySearch<T>(IList<T> array, int left, int right, T item, IComparer<T> comparer)
    {
        int mid = 0;

        while (right - left > 1)
        {
            mid = (left + right) >> 1; 

            if (comparer.Compare(array[mid], item) > 0)
            {
                right = mid - 1;
            }
            else
            {
                left = mid;
            }
        }

        if (comparer.Compare(array[left], item) == 0)
        {
            return left;
        }

        if (comparer.Compare(array[right], item) == 0)
        {
            return right;
        }

        return -1;
    }

    private static void DataValidation<T>(IList<T> array, int? left, int? right, IComparer<T> comparer)
    {
        if (array == null)
        {
            throw new ArgumentNullException($"{nameof(array)} can't be null");
        }

        if (left < 0 || right < 0 || left > right || right >= array.Count)
        {
            throw new ArgumentException("bounds are not valid");
        }

        if (comparer == null)
        {
            if (!typeof(IComparable).IsAssignableFrom(typeof(T))
             && !typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException("can't find a comparer for elements");
            }
        }

    }
}

