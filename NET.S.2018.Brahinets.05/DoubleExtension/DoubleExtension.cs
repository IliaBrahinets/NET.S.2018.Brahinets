using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DoubleExtension
{
    /// <summary>
    /// Returns the bit representation of the given double as string.
    /// </summary>
    public static string BitsRepresentationAsString(this double value)
    {
        long toLong = DoubleToLongBits(value);

        return LongToBitsString(toLong);
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

    private static unsafe long DoubleToLongBits(double d)
    {
        return *(long*)(&d);
    }
}

