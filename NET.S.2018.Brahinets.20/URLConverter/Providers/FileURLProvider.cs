using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class FileURLProvider : IURLAddresesProvider
    {
        private readonly string path;
        private readonly IURLParser parser;

        public FileURLProvider(string path, IURLParser parser)
        {
            this.path = path ?? throw new ArgumentNullException($"{nameof(path)} is null");
            this.parser = parser ?? throw new ArgumentNullException($"{nameof(parser)} is null");
            CheckExistance();
        }

        public IEnumerable<URL> GetURLs()
        {
            CheckExistance();

            return File.ReadLines(path).Select(x => parser.Parse(x));
        }

        private void CheckExistance()
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"a file by {path} can't be found");
            }
        }
    }
}
