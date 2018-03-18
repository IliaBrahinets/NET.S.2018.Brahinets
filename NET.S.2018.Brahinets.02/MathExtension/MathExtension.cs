using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MathExtension
{
    /// <summary>
    /// Returns the nth root of number, by Newton's method
    /// as a initial guess used number/n.
    /// </summary>
    /// <param name="n">The power of root must be more than 0.</param>
    /// <param name="precision">The precision needed to reach, must be more or equal to 0.</param>
    /// <param name="maxIterations">This is a limit to iterations performed by Newton's method, by default is 1000.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when n is less than or equal to 0 or precision is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when the root power is odd and the number is less than zero or the given precision can't be reached through the maxIterations.</exception>
    public static double FindNthRoot(double number, uint n, double precision, uint maxIterations = 1000)
    {
        if (n <= 0)
        {
            throw new ArgumentOutOfRangeException($"{ nameof(n) } must be > 0 ");
        }

        if ((n % 2 == 0) && number < 0)
        {
            throw new ArgumentException($"Invalid odd root calculation of number that is less than zero");
        }

        if (precision < 0)
        {
            throw new ArgumentOutOfRangeException($"{ nameof(precision) } must be >= 0");
        }

        ///xk+1 = (1/n)((n-1)xk + number/(xk^(n-1))
        ///where xk is the approximation of x^(1/n)
        double x_k = number / n;
        double x_kprev;
        double currPrecision;

        double reverseN = 1 / (double)n;

        do
        {
            if (maxIterations < 0)
            {
                throw new ArgumentException($"the number of iteration exceeds { nameof(maxIterations) }, " +
                                            $"the precision can't be reached");
            }

            x_kprev = x_k;
            x_k = (x_k * (n - 1) * reverseN) 
                + (reverseN * number / (Math.Pow(x_k, n - 1)));

            currPrecision = Math.Abs(x_k - x_kprev);

            maxIterations--;
        }
        while (currPrecision > precision);

        return x_k;
    }

    /// <summary>
    /// Returns the closest bigger number to the given number,
    /// if such number doesn't exist returns null.
    /// </summary>
    /// <param name="executionTime">Returned as ticks.</param>
    public static uint? FindNextBiggerNumber(uint number, out long executionTime)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        watch.Start();

        if(number == uint.MaxValue)
        {
            watch.Stop();
            executionTime = watch.ElapsedTicks;
            return null;
        }

        sbyte[] digits = new sbyte[10];

        uint digit = 0;
        uint prevDigit = number % 10;
        digits[prevDigit]++;

        number /= 10;
        
        bool alreadyMax = true;

        while (number != 0)
        {
            digit = number % 10;
            number /= 10;

            digits[digit]++;

            if (prevDigit > digit)
            {
                alreadyMax = false;
                break;
            }

            prevDigit = digit;
        }

        if (alreadyMax)
        {
            watch.Stop();
            executionTime = watch.ElapsedTicks;
            return null;
        }

        ///find the min but more than the digit
        for (uint i = digit + 1; i <= prevDigit; i++)
        {
            if (digits[i] != 0)
            {
                digits[i]--;
                number *= 10;
                number += i;
                break;
            }
        }

        ///sort the tail in ascending order
        for (uint i = 0; i <= 9; i++)
        {
            while (digits[i] != 0)
            {
                number *= 10;
                number += i;
                digits[i]--;
            }
        }

        watch.Stop();
        executionTime = watch.ElapsedTicks;
        return number;
    }
}