using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SortCriteriasAsStaticMethods
{
    /// <summary>
    /// Represent comparing two one dimensional arrays.
    /// As a key of comparing the sum of elements is used.  
    /// null is less than non-null.
    /// </summary>
    /// <returns>key(x) > key(y) -> more than zero
    ///          key(x) < key(y) -> less than zero
    ///          key(x) = key(y) -> zero
    ///          ***Ascending order***
    /// </returns>
    public static int ByRowsSum(int[] x, int[] y)
    {
        return new ByRowsSumComparer().Compare(x,y);
    }

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
    public static int ByRowsMax(int[] x, int[] y)
    {
        return new ByRowsMaxElemsComparer().Compare(x, y);
    }

    /// <summary>
    /// Represent comparing two one dimensional arrays.
    /// As a key of comparing the min elements of arrays is used.  
    /// null is less than non-null.
    /// </summary>
    /// <returns>key(x) > key(y) -> more than zero
    ///          key(x) < key(y) -> less than zero
    ///          key(x) = key(y) -> zero
    ///          ***Ascending order***
    /// </returns>
    public static int ByRowsMin(int[] x, int[] y)
    {
        return new ByRowsMaxElemsComparer().Compare(x, y);
    }
}

