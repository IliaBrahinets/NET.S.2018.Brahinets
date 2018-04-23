using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Solution
{
    public static class SequenceGenerator
    {
        public static IEnumerable<T> GetSequence<T>(T x1, T x2, int n, Func<T, T, T> recurrentRelation)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(n)} must be more or equal to 0");
            }

            if (n == 0)
            {
                return Enumerable.Empty<T>();
            }

            if(recurrentRelation == null)
            {
                throw new ArgumentNullException($"{nameof(recurrentRelation)} is null");
            }

            return ConstructSequence(x1, x2, n, recurrentRelation);
        }

        private static IEnumerable<T> ConstructSequence<T>(T x1, T x2, int n, Func<T, T, T> recurrentRelation)
        {
           
            yield return x1;

            if (n == 1)
            {
                yield break;
            }

            yield return x2;

            T curr = default(T);
            T xn = x2;
            T xn_1 = x1;

            for(int i = 3; i <= n; i++)
            {
                curr = recurrentRelation(xn, xn_1);
                yield return curr;
                xn_1 = xn;
                xn = curr;
            }

            yield break;
        }

    }
}
