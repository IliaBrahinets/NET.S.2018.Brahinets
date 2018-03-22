using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
public static class DoubleExtension
{
    
    public static string BitsRepresentationAsString(this double value)
    {

        byte signBit = TakeSign(value);
        int exponent = TakeExponent(value);
        long significand = TakeSignificand(value, exponent);

    }

    private static byte TakeSign(double value)
    {
        //1 - negative

        if (value == 0)
        {

            double causeInf = 1.0 / value;

            if (double.IsNegativeInfinity(causeInf))
            {
                return 1;
            }

            return 0;
        }

        if (value < 0)
            return 1;

        return 0;
    }

    private static int TakeExponent(double value)
    {
        double log2 = Math.Log(value, 2);

        int ExponentBorderBetweenNormAndSubNorm = -1022;

        if (log2 < ExponentBorderBetweenNormAndSubNorm)
        {
            return 0;
        }

        //the exponent's bias
        log2 += 1023;

        return (int)log2;
    }

    private static long TakeSignificand(double value, int exponent)
    {
        long significand = 0;

        if(exponent == 0)
        {
            ///[0..51]
            const int bits = 51;

            double power2 = GetPowerOf2(-bits - 1);

            long mask = (1 << bits);

            ///denormilizedValue
            while (value != 0)
            {
                value -= power2;

                significand |= mask;

                power2 *= 2;
                mask = mask >> 1;
            }
        }
        else
        {

        }
    }

    private static double GetPowerOf2(int p)
    {
        
        double answer = 1;

        if (p < 0)
        {
            for (int i = 1; i <= Math.Abs(p); i++)
            {
                answer /= 2;
            }
        }
        else
        {
            for (int i = 1; i <= p; i++)
            {
                answer *= 2;
            }
        }

        return answer;
    }
    
}
*/
