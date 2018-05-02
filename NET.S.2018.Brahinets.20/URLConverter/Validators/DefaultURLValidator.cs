using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class DefaultURLValidator : IURLValidator
    {
        public bool IsValid(string url)
        {
            //naive validation
            string[] parts = url.Split(new[] { "://", "/" }, StringSplitOptions.RemoveEmptyEntries);

            return !(parts.Length < 3);
        }
    }
}
