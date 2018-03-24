using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters
{
    public static class StringExtension
    {
        /// <summary>
        /// Convert the given number to the given notation.
        /// </summary>
        /// <exception cref="OverflowException">Thrown when the number exceeds Int.MaxValue.</exception>
        /// <exception cref="ArgumentException">Thrown when the number or the notation is null.</exception>
        public static int ToDecmialConverter(this string number, Notation notation)
        {
            ToDecmialConverterValidator(number, notation);

            if (number.Length == 0)
            {
                return 0;
            }

            number = number.ToUpper();

            int answer = 0;

            int currPowerOfBase = 0;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (currPowerOfBase == 0)
                {
                    currPowerOfBase = 1;
                }
                else
                {
                    checked
                    {
                        currPowerOfBase *= notation.Base;
                    }
                }

                char curr = number[i];

                if (notation.Alphabet.Contains(curr))
                {
                    int digit = GetNumeric(curr, notation);

                    checked
                    {
                        answer += digit * currPowerOfBase;
                    }
                }
                else
                {
                    throw new ArgumentException($"the symbol of given {nameof(number)} is not contained in the notation's alphabet");
                }
            }

            return answer;
        }

        private static void ToDecmialConverterValidator(string number, Notation notation)
        {
            if (number == null)
            {
                throw new ArgumentNullException($"{nameof(number)} is null");
            }

            if (notation == null)
            {
                throw new ArgumentNullException($"{nameof(notation)} is null");
            }
        }

        private static int GetNumeric(char symb, Notation notation)
        {
            return notation.Alphabet.IndexOf(symb);
        }
    }

    public class Notation
    {
        public Notation(int @base)
        {
            if (@base < MinBase || @base > MaxBase)
            {
                throw new ArgumentOutOfRangeException($"{nameof(@base)} must be >= {MinBase} and <= {MaxBase}");
            }

            Base = @base;
        }

        private int _Base;

        public int Base
        {
            get
            {
                return _Base;
            }

            set
            {
                _Base = value;
                CreateAlphabet();
            }
        }

        private const string _Alphabet = "0123456789ABCDEF";

        public string Alphabet { get; private set; }

        private const int MinBase = 2;

        private const int MaxBase = 16;

        private void CreateAlphabet()
        {
            Alphabet = _Alphabet.Substring(0, Base);
        }
    }
}