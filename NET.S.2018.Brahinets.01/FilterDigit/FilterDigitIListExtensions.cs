using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDigit
{
    public static class FilterDigitIListExtensions
    {
        /// <summary>
        /// Filters a sequence to contain numbers that contain the specified digits.
        /// </summary>
        /// <param name="digit">Must match 0 <= digit <= 9 </param>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="ArgumentException">The digit don't match 0 <= digit <= 9.</exception>
        /// <returns>The filtered sequence.</returns>
        public static IEnumerable<int> FilterDigit(this IEnumerable<int> seq, int digit)
        {
            if (seq == null)
            {
                throw new ArgumentNullException($"{nameof(seq)} is null!");
            }

            if (digit > 9)
            {
                throw new ArgumentException($"must be 0 <= {nameof(digit)} <= 9");
            }

            return FilterDigitYield(seq, digit);

        }

        private static IEnumerable<int> FilterDigitYield(IEnumerable<int> seq, int digit)
        {
            foreach (int item in seq)
            {
                if (IsNumberContainDigit(item, digit))
                {
                    yield return item;
                }
            }
        }

        private static bool IsNumberContainDigit(int number, int digit)
        {
            if (number < 0)
            {
                number *= -1;
            }

            do
            {
                if (number % 10 == digit)
                {
                    return true;
                }

                number = number / 10;
            }
            while (number != 0);

            return false;
        }
    }
}
