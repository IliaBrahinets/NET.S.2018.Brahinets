using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FibonacciGenerator
{
    /// <summary>
    /// Returns the first n numbers of the Fibonacci sequence.
    /// As the first and the second (0,1) or (1,1) is used, depend on the isFirstZero value.
    /// By default isFirstZero = false.
    /// </summary>
    /// <param name="isFirstZero">the first and the second elems of the sequence when true are (0,1), when false are (1,1).</param>
    /// <exception cref="ArgumentException">Thrown when length is less than zero.</exception>
    /// <exception cref="OverflowException">Thrown when some elem of series exceeded long.MaxValue.</exception>
    /// <returns></returns>
    public long[] GetSeries(int n, bool isFirstZero = false)
    {
        DataValidation(n);

        Tuple<byte, byte> seedValues = HandleSeedValues(isFirstZero);

        long[] series = new long[n];

        if(n == 0)
        {
            return series;
        }

        //F1
        series[0] = seedValues.Item1;

        if(n == 1)
        {
            return series;
        }

        //F2
        series[1] = seedValues.Item2;

        for(int i = 2; i < n; i++)
        {
            checked
            {
                series[i] = series[i - 1] + series[i - 2];
            }
        }

        return series;

    }

    private Tuple<byte, byte> HandleSeedValues(bool isFirstZero)
    {
        if (isFirstZero)
        {
            return new Tuple<byte, byte>(0, 1);
        }
        
        return new Tuple<byte, byte>(1, 1);
        
    }

    private void DataValidation(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException($"{nameof(n)} can't be < 0");
        }
    }
}

