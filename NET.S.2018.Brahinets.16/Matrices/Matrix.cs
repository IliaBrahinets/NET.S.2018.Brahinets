using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrices.Stores;

namespace Matrices
{
    public class Matrix<T>
    {
        #region Fields
        protected virtual IMatrixStore<T> store { get; }
        #endregion

        #region Constructors

        public Matrix(int n, int m)
        {
            BoundValidation(n,m);

            this.N = n;
            this.M = m;

            store = ConstructStore(n, m);
        }

        public Matrix(int n, int m, IEnumerable<Tuple<int, int, T>> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException($"{nameof(collection)} is null");
            }

            BoundValidation(n, m);

            this.N = n;
            this.M = m;

            store = ConstructStore(n, m);

            foreach (var elem in collection)
            {
                int i = elem.Item1;
                int j = elem.Item2;
                this[i, j] = elem.Item3;
            }
        }

        public Matrix(int n, int m, T[][] array)
        {
            BoundValidation(n, m);

            this.N = n;
            this.M = m;

            store = ConstructStore(n, m);

            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} is null");
            }

            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    this[i, j] = array[i][j];
                }
            }
        }

        protected void BoundValidation(int n, int m)
        {
            if (n <= 0 || m <= 0)
            {
                throw new IndexOutOfRangeException($"{nameof(N)} or {nameof(N)} is not valid");
            }
        }

        protected virtual IMatrixStore<T> ConstructStore(int n, int m)
        {
            return new DefaultMatrixStore<T>(n, m);
        }

        #endregion

        #region Events
        public event EventHandler<MatrixEventArgs> ValueChanged = delegate { };
        #endregion

        #region Properties
        public T this[int i, int j]
        {
            get
            {
                IndexesValidation(i, j);

                return GetElement(i, j);
            }
            set
            {
                IndexesValidation(i, j);

                SetElement(i, j, value);

            }
        }

        public virtual int N { get; }
        public virtual int M { get; }

        #endregion

        #region Methods

        #region Public

        #endregion

        #region Protected
        protected virtual T GetElement(int i, int j)
        {
            return store.GetElement(i, j);
        }

        protected virtual void SetElement(int i, int j, T value)
        {
            OnValueChanged(new MatrixEventArgs(i, j));
            store.SetElement(i, j, value);
        }

        protected virtual void OnValueChanged(MatrixEventArgs eventArgs)
        {
            ValueChanged(this, eventArgs);
        }

        protected bool IsIndexesValid(int i, int j)
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
