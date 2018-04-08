using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ByRowsMinElemsComparer : IComparer<int[]>
{
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
        return x.Min() - y.Min();
    }
}

