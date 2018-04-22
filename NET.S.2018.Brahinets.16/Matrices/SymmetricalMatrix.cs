using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrices.Stores;

namespace Matrices
{
    public class SymmetricalMatrix<T> : QuadMatrix<T>
    {
        #region Fields
        #endregion

        #region Constructors
        public SymmetricalMatrix(int n) : base(n)
        {

        }

        public SymmetricalMatrix(int n, IEnumerable<Tuple<int, int, T>> collection) : base(n, collection)
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
