using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class FakeURLProvider : IURLAddresesProvider
    {
        private readonly IURLParser parser;
        private readonly IURLValidator urlValidator;

        public FakeURLProvider(IURLParser parser, IURLValidator urlValidator)
        {
            this.parser = parser;
            this.urlValidator = urlValidator;
        }

        public IEnumerable<URL> GetURLs()
        {
            return new string[]
            {
                "https://github.com/AnzhelikaKravchuk?tab=repositories",
                "https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU",
                "https://habrahabr.ru/company/it-grad/blog/341486/"
            }.Select(x => parser.Parse(x));
        }
    }
}
