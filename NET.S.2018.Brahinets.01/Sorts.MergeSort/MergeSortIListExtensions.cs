using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorts.SortDataValidation;

namespace Sorts.MergeSort
{
    public static partial class MergeSortIListExtensions
    {
        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using merge sort algorithm, 
        /// as a comparer used the default comparer.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the array is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the type of sorting elements is not comparable.</exception>
        public static void MergeSort<T>(this IList<T> arr)
        {
            arr.MergeSort(null, null, null);
        }

        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using merge sort algorithm, 
        /// as a comparer used the presented comparison.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the array or comparison is null.</exception>
        public static void MergeSort<T>(this IList<T> arr, Comparison<T> comparison)
        {
            Comparer<T> comparer = Comparer<T>.Create(comparison);

            arr.MergeSort(null, null, comparer);
        }

        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using merge sort algorithm, 
        /// as a comparer used the presented comparer.
        /// </summary>
        /// <param name="comparer">If it is null, replaced by the default comparer.</param>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when comparer is null, and the presented type don't implement IComparable or it's generic analogue.</exception>
        public static void MergeSort<T>(this IList<T> arr, IComparer<T> comparer)
        {
            arr.MergeSort(null, null, comparer);
        }

        /// <summary>
        /// Sort the entire of any implementation of the generic IList interface using merge sort algorithm, 
        /// as a comparer used the presented comparer.
        /// </summary>
        /// <param name="left">Null is replaced on zero.</param>
        /// <param name="right">Null is replaced on the last index of the array.</param>
        /// <param name="comparer">Null is replaced by the default comparer.</param>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="ArgumentException">The presented bounds are not valid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when comparer is null, and the presented type don't implement IComparable or it's generic analogue.</exception>
        public static void MergeSort<T>(this IList<T> arr, int? left, int? right, IComparer<T> comparer)
        {
            SortDataValidator.Validate(arr, left, right, comparer);

            if (arr.Count == 0)
            {
                return;
            }

            int _left = left ?? 0;

            int _right = right ?? (arr.Count - 1);

            comparer = comparer ?? Comparer<T>.Default;

            MergeSort(arr, _left, _right, comparer);
        }

        private static void MergeSort<T>(IList<T> arr, int left, int right, IComparer<T> comparer)
        {
            if (left == right)
            {
                return;
            }

            int middle = (left + right) >> 1;

            MergeSort(arr, left, middle, comparer);
            MergeSort(arr, middle + 1, right, comparer);

            T[] buf = new T[right - left + 1];

            // pointers on the two parts of arr and the buffer
            int p1 = left;
            int p1max = middle;

            int p2 = middle + 1;
            int p2max = right;

            int pbuf = 0;

            while (p1 <= p1max && p2 <= p2max)
            {
                if (comparer.Compare(arr[p1], arr[p2]) < 0)
                {
                    buf[pbuf++] = arr[p1++];
                }
                else
                {
                    buf[pbuf++] = arr[p2++];
                }
            }

            while (p1 <= p1max)
            {
                buf[pbuf++] = arr[p1++];
            }

            while (p2 <= p2max)
            {
                buf[pbuf++] = arr[p2++];
            }

            for (int i = left; i <= right; i++)
            {
                arr[i] = buf[i - left];
            }
        }
    }
}
