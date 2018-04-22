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
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance with the give n sizes, and with default(T) as elements.
        /// </summary>
        /// <param name="n">Number of rows.</param>
        /// <param name="m">Number of collumns.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when n or m is equal or less than zero.</exception>
        public Matrix(int n, int m)
        {
            BoundValidation(n, m);

            this.N = n;
            this.M = m;

            store = ConstructStore(n, m);
        }

        /// <summary>
        /// Initialie an instance with the give sizes and the given elements.
        /// Meaning of the tuple is:
        /// tuple.item1 - i(row)
        /// tuple.item2 - j(collumn)
        /// tuple.item3 - value
        /// You shouldn't present a full matrix, elements are not given will be initialized with default(T).
        /// </summary>
        /// <param name="n">Number of rows.</param>
        /// <param name="m">Number of collumns.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when n or m is equal or less than zero.</exception>
        /// <exception cref="ArgumentNullException">Thrown when collection is null.</exception>
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

        /// <summary>
        /// Initialize an instance with the given sizes and the given array.
        /// You shouldn't provide all the matrix, but the sizes of the array must not be more the sizes of matrix.
        /// Elements was not presented will be initialized with default(T).
        /// </summary>
        /// <param name="n">Number of rows.</param>
        /// <param name="m">Number of collumns.</param>
        /// <param name="array">Elements of matrix.</param>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when n or m is equal or less than zero.</exception>
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

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i]?.Length; j++)
                {
                    this[i, j] = array[i][j];
                }
            }
        }

        protected Matrix()
        {

        }

        protected void BoundValidation(int n, int m)
        {
            if (n <= 0 || m <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(N)} or {nameof(N)} is not valid");
            }
        }

        protected virtual IMatrixStore<T> ConstructStore(int n, int m)
        {
            return new DefaultMatrixStore<T>(n, m);
        }

        #endregion

        #region Events
        /// <summary>
        /// Raise when a value somwhere(i,j) in the matrix was changed.
        /// </summary>
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
        protected virtual IMatrixStore<T> store { get; }

        #endregion

        #region Methods

        #region Public
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < N; i++)
            { 
                int j;
                for (j = 0; j < M - 1; j++)
                {
                    str.Append(GetElement(i, j).ToString() + ' ');
                }

                str.Append(GetElement(i, j));
                str.AppendLine();
            }

            return str.ToString();
        }
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
