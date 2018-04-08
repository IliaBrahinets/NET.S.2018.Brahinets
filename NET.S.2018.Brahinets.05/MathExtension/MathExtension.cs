using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MathExtension
{
    /// <summary>
    /// Calculates the greatest common divisor of two numbers by Euclid's algorithm, the non recursive implementation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when gcd(0, int.MinValue) or gcd(int.MinValue,int.MinValue) is calculated, because the answer is -int.MinValue and -int.MinValue = int.MaxValue + 1.</exception>
    public static int GcdByEuclid(int a, int b)
    {
        GcdDataValidationAndCorrection(ref a, ref b);

        while (a != 0 && b != 0)
        {
            if (a > b)
            {
                a = a % b;
            }
            else
            {
                b = b % a;
            }
        }

        return a + b;
    }

    /// <summary>
    /// Calculates the greatest common divisor of three numbers by Euclid's algorithm, the non recursive implementation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when gcd(0, int.MinValue) or gcd(int.MinValue,int.MinValue) is calculated, because the answer is -int.MinValue and -int.MinValue = int.MaxValue + 1.</exception>
    public static int GcdByEuclid(int a, int b, int c)
    {
        return GcdByEuclid(a, GcdByEuclid(b, c));
    }

    /// <summary>
    /// Calculates the greatest common divisor of many numbers by Euclid's algorithm, the non recursive implementation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when gcd(0, int.MinValue) or gcd(int.MinValue,int.MinValue) is calculated, because the answer is -int.MinValue and -int.MinValue = int.MaxValue + 1.</exception>
    public static int GcdByEuclid(params int[] numbers)
    {
        var gcdSolver = new GcdSolverDelegate(GcdByEuclid);
        return GcdOfMany(numbers, gcdSolver);
    }

    /// <summary>
    /// Calculates the greatest common divisor of two numbers by Stein's algorithm, the recursive implementation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when gcd(0, int.MinValue) or gcd(int.MinValue,int.MinValue) is calculated, because the answer is -int.MinValue and -int.MinValue = int.MaxValue + 1.</exception>
    public static int GcdByStein(int a, int b)
    {
        GcdDataValidationAndCorrection(ref a, ref b);
        return GcdBySteinRecursive(a, b);
    }

    /// <summary>
    /// Calculates the greatest common divisor of three numbers by Stein's algorithm, the non recursive implementation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when gcd(0, int.MinValue) or gcd(int.MinValue,int.MinValue) is calculated, because the answer is -int.MinValue and -int.MinValue = int.MaxValue + 1.</exception>
    public static int GcdByStein(int a, int b, int c)
    {
        return GcdByStein(a, GcdByStein(b, c));
    }

    /// <summary>
    /// Calculates the greatest common divisor of three numbers by Stein's algorithm, the non recursive implementation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when gcd(0, int.MinValue) or gcd(int.MinValue,int.MinValue) is calculated, because the answer is -int.MinValue and -int.MinValue = int.MaxValue + 1.</exception>
    public static int GcdByStein(params int[] numbers)
    {
        var gcdSolver = new GcdSolverDelegate(GcdByStein);
        return GcdOfMany(numbers, gcdSolver);
    }

    private static int GcdBySteinRecursive(int a, int b)
    {
        if (a == 0)
        {
            return b;
        }

        if (b == 0)
        {
            return a;
        }

        if (a == b)
        {
            return a;
        }

        if (a == 1 || b == 1)
        {
            return 1;
        }

        if ((a & 1) == 0)
        {
            a = a >> 1;

            if ((b & 1) == 0)
            {
                return GcdByStein(a, b >> 1) << 1;
            }

            return GcdByStein(a, b);
        }

        if ((b & 1) == 0)
        {
            return GcdByStein(a, b >> 1);
        }

        if (a > b)
        {
            return GcdByStein((a - b) >> 1, b);
        }

        return GcdByStein((b - a) >> 1, a);
    }

    private static int GcdOfMany(int[] numbers, GcdSolverDelegate gcdSolver)
    {
        int gcd = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            gcd = gcdSolver(gcd, numbers[i]);

            if (gcd == 1)
            {
                return 1;
            }
        }

        return gcd;
    }

    private static void GcdDataValidationAndCorrection(ref int a, ref int b)
    {
        if (a == int.MinValue && b == int.MinValue)
        {
            throw new ArgumentException("Gcd(int.MinValue,int.MinValue) = -int.MinValue, but int can't represent it" +
                                        " because of -int.MinValue = int.MaxValue + 1");
        }

        if (a == int.MinValue && b == 0 || b == int.MinValue && a == 0)
        {
            throw new ArgumentException("Gcd(0, int.MinValue) = -int.MinValue, but int can't represent it" +
                                        " because of -int.MinValue = int.MaxValue + 1");
        }

        if (a < 0)
        {
            ///this is to handle Int.MinValue cases
            ///because int can't represent -int.MinValue
            if (b != 0)
            {
                a = a % b;
            }

            a = -a;
        }

        if (b < 0)
        {
            ///this is to handle Int.MinValue cases
            ///because int can't represent -Int.MinValue
            if (a != 0)
            {
                b = b % a;
            }

            b = -b;
        }
    }
}