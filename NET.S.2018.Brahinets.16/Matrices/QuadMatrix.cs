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
        /// <summary>
        /// Initialize a new quad matrix with the give n size, and with default(T) as elements.
        /// </summary>
        /// <param name="N">Size of the quad matrix.</param>
        public QuadMatrix(int N) : base(N, N)
        {
        }

        public QuadMatrix(int N, IEnumerable<Tuple<int, int, T>> collection) : base(N, N, collection)
        {
        }

        public QuadMatrix(int n, T[][] array):base(n,n,array)
        {
        }

        protected QuadMatrix()
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
