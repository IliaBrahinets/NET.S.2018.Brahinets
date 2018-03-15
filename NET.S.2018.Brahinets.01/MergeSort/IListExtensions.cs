using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    public static partial class IListExtensions
    {
        public static void MergeSort<T>(this IList<T> arr)
        {
            arr.MergeSort(null, null, null);
        }

        public static void MergeSort<T>(this IList<T> arr, Comparison<T> comparison)
        {
            Comparer<T> comparer = Comparer<T>.Create(comparison);

            arr.MergeSort(null, null, comparer);
        }

        public static void MergeSort<T>(this IList<T> arr, IComparer<T> comparer)
        {
            arr.MergeSort(null, null, comparer);
        }

        public static void MergeSort<T>(this IList<T> arr, int? left, int? right, IComparer<T> comparer)
        {
            if (arr == null)
            {
                throw new ArgumentNullException("Array is null!");
            }

            if (arr.Count == 0)
                return;

            if (left < 0 || right < 0 || left > right || right >= arr.Count)
            {
                throw new ArgumentException("Bounds are not valid!");
            }

            int _left = left ?? 0;

            int _right = right ?? (arr.Count - 1);

            comparer = comparer ?? Comparer<T>.Default;

            if (comparer == null)
            {
                throw new InvalidOperationException("a Comparison or Comparer is not found!");
            }

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
