using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class SymmetricalMatrix<T> : QuadMatrix<T>
    {
        public SymmetricalMatrix(int N)
        {
            if(N <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(N)} is not valid");
            }

            this.N = N;
            this.M = N;

            array = new T[N][];

            for(int i = 0; i < N; i++)
            {
                array[i] = new T[i+1];
            }
        }

        public SymmetricalMatrix(int N, IEnumerable<Tuple<int, int, T>> collection)
        {

        }

        protected override T GetElement(int i, int j)
        {
            return base.GetElement(i, j);
        }

        protected override bool IsIndexesValid(int i, int j)
        {
            return base.IsIndexesValid(i, j);
        }

        protected override void SetElement(int i, int j, T value)
        {
            base.SetElement(i, j, value);
        }
    }
}
