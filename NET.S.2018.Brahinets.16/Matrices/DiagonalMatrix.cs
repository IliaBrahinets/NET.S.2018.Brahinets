using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrices.Stores;

namespace Matrices
{
    class DiagonalMatrix<T>:QuadMatrix<T>
    {
        #region Fields

        #endregion

        #region Constructors
        public DiagonalMatrix(int N) : base(N)
        {
        }

        public DiagonalMatrix(int N, IEnumerable<Tuple<int, int, T>> collection) : base(N, collection)
        {
        }

        public DiagonalMatrix(int n, T[][] array) : base(n, array)
        {
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

        #endregion

        #region Methods

        #region Public

        #endregion

        #region Private

        #endregion

        #endregion
    }
}
