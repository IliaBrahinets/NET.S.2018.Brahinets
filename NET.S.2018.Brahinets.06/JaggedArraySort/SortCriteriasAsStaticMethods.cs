using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SortCriteriasAsStaticMethods
{

    public static int ByRowsSum(int[] x, int[] y)
    {
        return new ByRowsSumComparer().Compare(x,y);
    }

    public static int ByRowsMax(int[] x, int[] y)
    {
        return new ByRowsMaxElemsComparer().Compare(x, y);
    }

    public static int ByRowsMin(int[] x, int[] y)
    {
        return new ByRowsMaxElemsComparer().Compare(x, y);
    }
}

