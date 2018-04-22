using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrices.Stores;

namespace Matrices
{
    public class DiagonalMatrix<T>:QuadMatrix<T>
    {
        #region Fields

        #endregion

        #region Constructors
        public DiagonalMatrix(int n) : base(n)
        {
        }

        public DiagonalMatrix(int n, T[] array):base(n)
        {
            if(array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} is null");
            }

            if(array.Length > n)
            {
                throw new ArgumentException($"size of {nameof(array)} more than {nameof(n)}");
            }

            store = ConstructStore(n, n);

            for(int i = 0; i < array.Length; i++)
            {
                this[i, i] = array[i];
            }
        }

        protected override IMatrixStore<T> ConstructStore(int n, int m)
        {
            return new DiagonalMatrixStore<T>(n);
        }

        protected override void SetElement(int i, int j, T value)
        {
            if(i != j)
            {
                throw new InvalidOperationException("a diagonal matrix have non-default elements only on the diagonal");
            }

            base.SetElement(i, j, value);
        }
        #endregion

        #region Properties
        public override int N { get; }
        public override int M { get; }
        protected override IMatrixStore<T> store { get; }
        #endregion

        #region Methods

        #region Public

        #endregion

        #region Private

        #endregion

        #endregion
    }
}
