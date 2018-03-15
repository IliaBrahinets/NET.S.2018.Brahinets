using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorts.SortDataValidation
{
    public static partial class SortDataValidator
    {
        public static void Validate<T>(IList<T> arr, int? left, int? right, IComparer<T> comparer)
        {
            if (arr == null)
            {
                throw new ArgumentNullException("Array is null!");
            }

            if (left < 0 || right < 0 || left > right || right >= arr.Count)
            {
                throw new ArgumentException("Bounds are not valid!");
            }

            if (comparer == null)
            {
                comparer = Comparer<T>.Default;

                if (!typeof(IComparable).IsAssignableFrom(typeof(T))
                    && !typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                {
                    throw new InvalidOperationException("Cant compare elements!");
                }
            }
        }
    }
}
