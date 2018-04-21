using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public class Matrix<T>
    {
        #region Fields
        protected T[][] array;
        #endregion

        #region Constructors

        public Matrix(int N, int M)
        {
            if (N <= 0 || M <= 0)
            {
                throw new IndexOutOfRangeException($"{nameof(N)} or {nameof(N)} is not valid");
            }

            this.N = N;
            this.M = M;

            array = new T[M][];

            for (int i = 0; i < N; i++)
            {
                array[i] = new T[N];
            }
        }

        public Matrix(int N, int M, IEnumerable<Tuple<int, int, T>> collection) : this(N, M)
        {
            if (collection == null)
            {
                throw new ArgumentNullException($"{nameof(collection)} is null");
            }

            foreach (var elem in collection)
            {
                int i = elem.Item1;
                int j = elem.Item2;
                this[i, j] = elem.Item3;
            }
        }

        protected Matrix()
        {

        }

        #endregion

        #region Properties
        public T this[int i, int j]
        {
            get
            {
                BoundsValidation(i, j);

                return GetElement(i, j);
            }
            set
            {
                BoundsValidation(i, j);

                SetElement(i, j, value);

            }
        }

        public int N { get; protected set; }
        public int M { get; protected set; }

        #endregion

        #region Methods

        #region Public

        #endregion

        #region Protected
        protected virtual T GetElement(int i, int j)
        {
            return array[i][j];
        }

        protected virtual void SetElement(int i, int j, T value)
        {
            array[i][j] = value;
        }

        protected virtual bool IsIndexesValid(int i, int j)
        {
            if (i < 0 || i >= N || j < 0 || j >= M)
            {
                return false;
            }

            return true;
        }
        protected void IndexesValidation(int i, int j)
        {
            if (!IsIndexesValid(i, j))
            {
                throw new IndexOutOfRangeException($"{nameof(i)} or {nameof(j)} is not valid");
            }
        }
        #endregion

        #region Private

        #endregion

        #endregion
    }
}
