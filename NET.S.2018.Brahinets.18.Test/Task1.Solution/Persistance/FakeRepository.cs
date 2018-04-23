using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Solution.Persistance
{
    public class FakeRepository : IRepository
    {
        public void Create(string password)
        {
            Console.WriteLine($"Password({password}) was added");
        }
    }
}
