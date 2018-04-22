using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrices.Stores;

namespace Matrices
{
    class SymmetricalMatrix<T> : QuadMatrix<T>
    {
        #region Fields
        #endregion

        #region Constructors
        public SymmetricalMatrix(int N) : base(N)
        {

        }

        public SymmetricalMatrix(int N, IEnumerable<Tuple<int, int, T>> collection) : base(N, collection)
        {

        }

        protected override IMatrixStore<T> ConstructStore(int n, int m)
        {
            return new SymmetricalMatrixStore<T>(n);
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
