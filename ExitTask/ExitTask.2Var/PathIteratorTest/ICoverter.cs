using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathIteratorTest
{
    interface ICoverter<in TInput, out TOutput>
    {
        TOutput Convert(TInput input);
    }
}
