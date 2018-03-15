using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDigit
{
    public static class IListExtensions
    {
        /// <summary>
        /// Filters the sequence to contain digits that contain the specified number.
        /// </summary>
        /// <param name="number">Must match 0 <= number <= 9 </param>
        /// <exception cref="ArgumentNullException">The array is null.</exception>
        /// <exception cref="ArgumentException">The number don't match 0 <= number <= 9.</exception>
        /// <returns>The filtered sequence.</returns>
        public static IList<int> FilterDigit(this IList<int> arr, int number)
        {
            if (arr == null)
            {
                throw new ArgumentNullException("Array is null!");
            }

            if (number > 9)
            {
                throw new ArgumentException("must be 0 <= number <= 9");
            }

            List<int> answer = new List<int>();

            for (int i = 0; i < arr.Count; i++)
            {
                if (IsNumberContainDigit(arr[i], number))
                {
                    answer.Add(arr[i]);
                }
            }

            return answer;
        }

        private static bool IsNumberContainDigit(int digit, int number)
        {
            if (digit < 0)
            {
                digit *= -1;
            }

            do
            {
                if (digit % 10 == number)
                {
                    return true;
                }

                digit = digit / 10;
            }
            while (digit != 0);

            return false;
        }
    }
}
