using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorts.SortDataValidation;

namespace Sorts.QuickSort
{
    public static partial class QuickSortIListExtensions
    {
        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using quicksort algorithm, 
        /// as a comparer used the default comparer.
        /// </summary>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="InvalidOperationException">The type of sorting elements is not comparable.</exception>
        public static void QuickSort<T>(this IList<T> arr)
        {
            arr.QuickSort(null, null, null);
        }

        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using quicksort algorithm, 
        /// as the comparer used presented comparison.
        /// </summary>
        /// <exception cref="ArgumentNullException">The array or comparison is null.</exception>
        public static void QuickSort<T>(this IList<T> arr, Comparison<T> comparison)
        {
            Comparer<T> comparer = Comparer<T>.Create(comparison);

            arr.QuickSort(null, null, comparer);
        }

        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using quicksort algorithm, 
        /// as the comparer used the presented comparer.
        /// </summary>
        /// <param name="comparer">Null, replaced by the default comparer.</param>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="InvalidOperationException">Comparer is null, and the presented type don't implement IComparable or it's generic analogue.</exception>
        public static void QuickSort<T>(this IList<T> arr, IComparer<T> comparer)
        {
            arr.QuickSort(null, null, comparer);
        }

        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using quicksort algorithm, 
        /// as a comparer used the presented comparer.
        /// </summary>
        /// <param name="left">Null is replaced on zero.</param>
        /// <param name="right">Null is replaced on the last index of the array.</param>
        /// <param name="comparer">Null is replaced by the default comparer.</param>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="ArgumentException">The presented bounds are not valid.</exception>
        /// <exception cref="InvalidOperationException">Comparer is null, and the presented type don't implement IComparable or it's generic analogue.</exception>
        public static void QuickSort<T>(this IList<T> arr, int? left, int? right, IComparer<T> comparer)
        {
            SortDataValidator.Validate(arr, left, right, comparer);

            if (arr.Count == 0)
            {
                return;
            }

            int _left = left ?? 0;

            int _right = right ?? (arr.Count - 1);

            comparer = comparer ?? Comparer<T>.Default;

            QuickSort(arr, _left, _right, comparer);
        }

        private static void QuickSort<T>(IList<T> arr, int left, int right, IComparer<T> comparer)
        {
            int mid = (left + right) >> 1;

            T pivot = arr[mid];

            int i = left;
            int j = right;

            while (i <= j)
            {
                while (comparer.Compare(arr[i], pivot) < 0)
                {
                    i++;
                }

                while (comparer.Compare(arr[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    T tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                QuickSort(arr, left, j, comparer);
            }

            if (i < right)
            {
                QuickSort(arr, i, right, comparer);
            }
        }
    }
}
