using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public interface IURLAddresesProvider
    {
        IEnumerable<URL> GetURLs();
    }
}
