using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DoubleEqualityExtension
{
    /// <summary>
    /// Compare doubles considering the given accuracy.
    /// </summary>
    /// <param name="accuracy">The max acceptable diffrenece between the numbers.</param>
    public static bool AccurateEquals(this double number, double otherNumber, double accuracy)
    {
        ///it's mostly to handle the special values(NaN,-inf,+inf)
        if (number == otherNumber)
        {
            return true;
        }

        if(Math.Abs(number - otherNumber) <= accuracy)
        {
            return true;
        }

        return false;
    }
}

