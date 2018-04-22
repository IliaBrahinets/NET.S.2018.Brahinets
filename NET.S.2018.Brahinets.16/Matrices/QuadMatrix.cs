using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public class QuadMatrix<T> : Matrix<T>
    {
        #region Fields

        #endregion

        #region Constructors
        public QuadMatrix(int N) : base(N, N)
        {
        }

        public QuadMatrix(int N, IEnumerable<Tuple<int, int, T>> collection) : base(N, N, collection)
        {
        }

        public QuadMatrix(int n, T[][] array):base(n,n,array)
        {
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
