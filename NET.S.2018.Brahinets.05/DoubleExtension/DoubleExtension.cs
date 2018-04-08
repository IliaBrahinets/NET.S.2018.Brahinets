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

        return toLong.LongToBitsString();
    }

    private static unsafe long DoubleToLongBits(double d)
    {
        return *(long*)(&d);
    }
}

