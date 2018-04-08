using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class LongExtension
{
    public static string LongToBitsString(this long value)
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
}

