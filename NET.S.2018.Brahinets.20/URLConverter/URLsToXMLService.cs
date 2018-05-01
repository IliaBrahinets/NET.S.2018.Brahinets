using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace URLParser
{
    public class URLsToXMLService
    {
        private IURLAddresesProvider provider;
        private IURLConverterToXML converter;

        public URLsToXMLService(IURLAddresesProvider provider, IURLConverterToXML converter)
        {
            this.provider = provider;
            this.converter = converter;
        }

        public XDocument Convert()
        {
            XDocument document = new XDocument(new XDeclaration("1.0", "UTF-8", null));

            XElement urlsRoot = new XElement("urlAddresses");
            document.Add(urlsRoot);

            foreach (var url in provider.GetURLs())
            {
                XElement converted = converter.Convert(url);

                urlsRoot.Add(converted);
            }

            return document;
        }
    }
}
