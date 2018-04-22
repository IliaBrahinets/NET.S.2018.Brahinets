using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices.Stores
{
    class SymmetricalMatrixStore<T> : IMatrixStore<T>
    {
        #region Fields
        private readonly T[][] array;
        #endregion

        #region Constructors
        public SymmetricalMatrixStore(int n)
        {
            this.N = n;
            this.M = n;

            array = new T[N][];
            for(int i = 0; i < N; i++)
            {
                array[i] = new T[i + 1];
            }

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
            if(j > i)
            {
                int tmp = i;
                i = j;
                j = tmp;
            }

            return array[i][j];
        }

        public void SetElement(int i, int j, T element)
        {
            if (j > i)
            {
                int tmp = i;
                i = j;
                j = tmp;
            }

            array[i][j] = element;
        }
        #endregion

        #region Private

        #endregion

        #endregion



    }
}
