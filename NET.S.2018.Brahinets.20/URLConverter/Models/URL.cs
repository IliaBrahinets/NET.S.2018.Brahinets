using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class URL
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public IEnumerable<string> UrlPathSegments { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<KeyValuePair<string, string>> Parameters { get; set; } = Enumerable.Empty<KeyValuePair<string, string>>();
    }
}
