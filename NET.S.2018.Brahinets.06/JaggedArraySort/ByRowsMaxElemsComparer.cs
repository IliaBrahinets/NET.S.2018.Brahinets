using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ByRowsMaxElemsComparer : IComparer<int[]>
{
    /// <summary>
    /// Represent comparing two one dimensional arrays.
    /// As a key of comparing the max elements of arrays is used.  
    /// null is less than non-null.
    /// </summary>
    /// <returns>key(x) > key(y) -> more than zero
    ///          key(x) < key(y) -> less than zero
    ///          key(x) = key(y) -> zero
    ///          ***Ascending order***
    /// </returns>
    public int Compare(int[] x, int[] y)
    {
        if (x == y)
        {
            return 0;
        }

        if (x == null || y == null)
        {
            if (x == null)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        return x.Max() - y.Max();
    }
}

