using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Solution
{
    public interface IPasswordCondition
    {
        Tuple<bool,string> IsValid(string password);
    }
}
