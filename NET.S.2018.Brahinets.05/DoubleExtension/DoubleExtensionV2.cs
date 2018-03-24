using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class DoubleExtensionV2
{

    private const int exponentBias = 1023;

    public static string BitsRepresentationAsStringV2(this double value)
    {
        string trySpecialValues = TrySpecialValues(value);

        if(trySpecialValues != null)
        {
            return trySpecialValues;
        }

        long signBit = TakeSign(value);
        long exponent = TakeExponent(value);
        long significand = TakeSignificand(value, (int)exponent);
         
        return LongToBitsString((signBit << 63) | (exponent << 52) | significand);

    }

    private static string TrySpecialValues(double value)
    {
        if (double.IsPositiveInfinity(value))
        {
            return LongToBitsString(0x7FF0000000000000);
        }

        if (double.IsNegativeInfinity(value))
        {
            return LongToBitsString(0xFFF0000000000000);
        }

        if (double.IsNaN(value))
        {
            return LongToBitsString(0xFFF8000000000000);
        }

        if(double.MaxValue == value)
        {
            return LongToBitsString(0x7FEFFFFFFFFFFFFF);
        }
        
        if(double.MinValue == value)
        {
            return LongToBitsString(0xFFEFFFFFFFFFFFFF);
        }

        return null;
    }
    private static string LongToBitsString(long value)
    {
        const int bitsLength = 64;

        StringBuilder answer = new StringBuilder();

        long mask = 1L << (bitsLength - 1);

        for (long i = 1; i <= bitsLength; i++)
        {
            if ((value & mask) != 0)
            {
                answer.Append('1');
            }
            else
            {
                answer.Append('0');
            }

            value = value << 1;

        }

        return answer.ToString();
    }
    private static string LongToBitsString(ulong value)
    {
        const int bitsLength = 64;

        StringBuilder answer = new StringBuilder();

        ulong mask = 1uL << (bitsLength - 1);

        for (long i = 1; i <= bitsLength; i++)
        {
            if ((value & mask) != 0)
            {
                answer.Append('1');
            }
            else
            {
                answer.Append('0');
            }

            value = value << 1;

        }

        return answer.ToString();
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
        {
            return 1;
        }

        return 0;
    }

    private static int TakeExponent(double value)
    { 
        if (value < 0)
        {
            value = -value;
        }

        double log2 = Math.Log(value, 2);

        int ExponentBorderBetweenNormAndSubNorm = -exponentBias - 1;

        if (log2 < ExponentBorderBetweenNormAndSubNorm)
        {
            return 0;
        }

        log2 = Math.Floor(log2);

        //the exponent's bias
        log2 += exponentBias;

        return (int)log2;
    }

    private static long TakeSignificand(double value, int exponent)
    {
        long significand = 0;

        if(value < 0)
        {
            value = -value;
        }

        ///[0..51]
        const int bits = 52;

        double twoPowExponent;

        if (exponent != 0)
        {
            twoPowExponent = GetPowerOfTwo(exponent - exponentBias);
        }
        else
        {
            twoPowExponent = GetPowerOfTwo(-exponentBias + 1);
        }

        value /= twoPowExponent;

        if (exponent != 0)
        {
            value -= 1;
        }

        double currPowerOfTwo = 1.0 / 2;

        long mask = 1L << (bits - 1);

        for (int i = 1; i <= bits; i++)
        {
            if (currPowerOfTwo <= value)    
            {
                value -= currPowerOfTwo;

                significand |= mask;
            }

            if (value == twoPowExponent)
            {
                break;
            }

            currPowerOfTwo /= 2;
            mask = mask >> 1;

        }

        return significand;

    }

    private static double GetPowerOfTwo(int p)
    {
        double answer = 1;

        if (p < 0)
        {
            int absP = Math.Abs(p);
            for (int i = 1; i <= absP; i++)
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

