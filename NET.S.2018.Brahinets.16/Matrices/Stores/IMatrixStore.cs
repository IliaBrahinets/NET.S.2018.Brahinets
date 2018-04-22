using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices.Stores
{
    public interface IMatrixStore<T>
    {
        int N { get; }
        int M { get; }

        void SetElement(int i, int j, T element);
        T GetElement(int i, int j);
    }
}
