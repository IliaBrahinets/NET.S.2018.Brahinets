using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices.Stores
{
    class DiagonalMatrixStore<T> : IMatrixStore<T>
    {
        #region Fields
        private readonly T[] array;
        #endregion

        #region Constructors
        public DiagonalMatrixStore(int n)
        {
            this.N = n;
            this.M = M;

            array = new T[n];
        }
        #endregion

        #region Properties
        public int N { get; }
        public int M { get; }
        #endregion

        #region Methods

        #region Public
        public T GetElement(int i, int j)
        {
            if (i != j)
            {
                return default(T);
            }

            return array[i];
        }

        public void SetElement(int i, int j, T element)
        {
            array[i] = element;
        }
        #endregion

        #region Private

        #endregion

        #endregion



    }
}
