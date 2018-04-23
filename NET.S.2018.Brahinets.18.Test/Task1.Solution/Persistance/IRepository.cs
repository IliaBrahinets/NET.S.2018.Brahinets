using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Solution.Persistance
{
    public interface IRepository
    {
        void Create(string password);
    }
}
