using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FibonacciGenerator
{
    /// <summary>
    /// Returns the first n numbers of the Fibonacci sequence.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when length is less than zero.</exception>
    /// <exception cref="OverflowException">Thrown when some elem of series exceeded long.MaxValue.</exception>
    /// <returns></returns>
    public long[] GetSeries(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException($"{nameof(n)} can't be < 0");
        }

        long[] series = new long[n];

        if(n == 0)
        {
            return series;
        }

        //F1
        series[0] = 1;

        if(n == 1)
        {
            return series;
        }

        //F2
        series[1] = 1;

        for(int i = 2; i < n; i++)
        {
            checked
            {
                series[i] = series[i - 1] + series[i - 2];
            }
        }

        return series;

    }
}

