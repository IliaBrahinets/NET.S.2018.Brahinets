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
    /// <param name="mode">Determines a mode of sorting.</param>
    public static void Sort(int[][] array, SortMode mode)
    { 
        bool isAlreadySorted = true;

        int lastIndex = array.Length - 1;

        for (int j = lastIndex; j >= 0; j--)
        {
            for (int i = 0; i < j; i++)
            {
                if (mode.Compare(array[i], array[i + 1]) > 0)
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

    private static void Swap<T>(ref T A, ref T B)
    {
        T tmp = A;
        A = B;
        B = tmp;
    }

    public enum SortOrder
    {
        AscendingOrder = 1,
        DecreasingOrder = -1
    }

    public abstract class SortMode
    {
        /// <summary>
        /// Determines an order of a sort.Ascending or Decreasing.
        /// </summary>
        public SortOrder SortOrder { get; }

        /// <summary>
        /// Initialize a sort mode with the default sort order.
        /// </summary>
        public SortMode()
        {
            SortOrder = DefaultSortOrder;
        }

        /// <summary>
        /// Initilize a sort mode with the given sort order.
        /// </summary>
        public SortMode(SortOrder sortOrder) : this()
        {
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Provides a comparasion of two elements. 
        /// </summary>
        /// <return>a > b -> greater than zero
        ///         a < b -> less than zero
        ///         a = b -> zero
        /// </returns>
        public int Compare(int[] a, int[] b)
        {
            int order = (int)SortOrder;

            if (a == b)
            {
                return 0;
            }

            if (a == null || b == null)
            {
                if (b == null)
                {
                    return order;
                }
                else
                {
                    return -order;
                }
            }

            return order * CompareCriteria(a, b);

        }

        /// <summary>
        /// This is a comparasion's criteria.
        /// Returns values for sorting by ascending order by the criteria.
        /// it does't handle null's cases, sort order, represents only criteria.
        /// Used by the compare method.
        /// </summary>
        protected abstract int CompareCriteria(int[] a, int[] b);

        private const SortOrder DefaultSortOrder = SortOrder.AscendingOrder;
    }

    public class SortByRowsSum : SortMode
    {
        public SortByRowsSum():base()
        { }

        public SortByRowsSum(SortOrder sortOrder):base(sortOrder)
        { }

        protected override int CompareCriteria(int[] a, int[] b)
        {
            return a.Sum() - b.Sum();
        }
    }

    public class SortByRowsMaxElems:SortMode
    {
        public SortByRowsMaxElems() : base()
        { }

        public SortByRowsMaxElems(SortOrder sortOrder) : base(sortOrder)
        { }

        protected override int CompareCriteria(int[] a, int[] b)
        {
            return a.Max() - b.Max();
        }
    }

    public class SortByRowsMinElems : SortMode
    {
        public SortByRowsMinElems() : base()
        { }

        public SortByRowsMinElems(SortOrder sortOrder) : base(sortOrder)
        { }

        protected override int CompareCriteria(int[] a, int[] b)
        {
            return a.Min() - b.Min();
        }
    }
}

