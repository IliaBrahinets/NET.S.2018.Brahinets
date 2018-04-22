using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices.Stores
{
    public class DefaultMatrixStore<T> : IMatrixStore<T>
    {
        #region Fields
        private readonly T[][] array;
        #endregion

        #region Constructors
        public DefaultMatrixStore(int N, int M)
        {
            this.N = N;
            this.M = M;

            array = new T[M][];

            for (int i = 0; i < N; i++)
            {
                array[i] = new T[N];
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
            return array[i][j];
        }

        public void SetElement(int i, int j, T element)
        {
            array[i][j] = element;
        }
        #endregion

        #region Private

        #endregion

        #endregion

    }
}
