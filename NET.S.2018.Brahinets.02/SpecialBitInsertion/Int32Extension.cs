using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialBitInsertion
{
    public static class Int32Extension
    {
        /// <summary>
        /// Inserting one number into another so that the second number occupies the position from bit j to bit i (bits are numbered from right to left).
        /// </summary>
        /// <param name="numberSource">The first number.</param>
        /// <param name="numberIn">The second number.</param>
        /// <exception cref="ArgumentOutOfRangeException">i or j is less than zero or more than 31.</exception>
        /// <exception cref="ArgumentException">i is more than j.</exception>
        public static int SpecialBitInsertion(int numberSource, int numberIn, int i, int j)
        {
            if (i < 0 || j < 0 || i > 31 || j > 31)
            {
                throw new ArgumentOutOfRangeException($"{ nameof(i) } or { nameof(j) } is out of the range[0..31]");
            }

            if (i > j)
            { 
                throw new ArgumentException($"values of { nameof(i) },{ nameof(j) } are not matched to { nameof(j) } > { nameof(i) }");
            }

            int answer = numberSource;

            for (int k = i; k <= j; k++)
            {
                int inMask = 1 << (k - i);

                int sourceMask = 1 << k;

                if ((numberIn & inMask) != 0)
                {
                    answer |= sourceMask;
                }
                else
                {
                    if ((numberSource & sourceMask) != 0)
                    {
                        answer ^= sourceMask;
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// Inserting one number into another so that the second number occupies the position from bit j to bit i (bits are numbered from right to left).
        /// </summary>
        /// <param name="numberSource">The first number.</param>
        /// <param name="numberIn">The second number.</param>
        /// <exception cref="ArgumentOutOfRangeException">i or j is less than 0 or more than 31.</exception>
        /// <exception cref="ArgumentException">i is more than j.</exception>
        public static int SpecialBitInsertionV2(int numberSource, int numberIn, int i, int j)
        {
            if (i < 0 || j < 0 || i > 31 || j > 31)
            {
                throw new ArgumentOutOfRangeException($"{ nameof(i) } or { nameof(j) } is out of the range[0..31]");
            }

            if (i > j)
            {
                throw new ArgumentException($"values of { nameof(i) },{ nameof(j) } are not matched to { nameof(j) } > { nameof(i) }");
            }

            int unitMask = 2;

            for (int k = 1; k <= (j - i); k++)
            {
                unitMask *= 2;
            }

            unitMask -= 1;
            
            int answer = numberSource;

            int partOfInNumber = unitMask & numberIn;

            answer = (partOfInNumber << i) | (numberSource & ~unitMask);

            return answer;
        }
    }
}
