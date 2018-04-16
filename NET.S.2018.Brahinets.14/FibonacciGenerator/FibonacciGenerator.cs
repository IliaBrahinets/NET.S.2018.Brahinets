using System;
using System.Collections.Generic;
using System.Numerics;
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
    /// <exception cref="ArgumentException">Thrown when n is less than zero.</exception>
    /// <returns></returns>
    public static IEnumerable<BigInteger> GetSeries(int n, bool isFirstZero = false)
    {
        DataValidation(n);

        Tuple<byte, byte> seedValues = HandleSeedValues(isFirstZero);
       
        if(n == 0)
        {
            yield break;
        }

        BigInteger fn_2 = seedValues.Item1;
        yield return fn_2;

        //Fn-2
        if (n == 1)
        {
            yield break;
        }

        //Fn-1
        BigInteger fn_1 = seedValues.Item2;
        yield return fn_1;

        BigInteger curr;

        for (int i = 2; i < n; i++)
        {
            curr = fn_1 + fn_2;
            yield return curr;

            fn_2 = fn_1;
            fn_1 = curr;
        }
    }

    private static Tuple<byte, byte> HandleSeedValues(bool isFirstZero)
    {
        if (isFirstZero)
        {
            return new Tuple<byte, byte>(0, 1);
        }
        
        return new Tuple<byte, byte>(1, 1);
        
    }

    private static void DataValidation(int n)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(n)} can't be < 0");
        }
    }
}

