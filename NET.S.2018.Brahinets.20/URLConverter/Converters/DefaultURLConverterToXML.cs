using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace URLParser
{
    public class DefaultURLConverterToXML : IURLConverterToXML
    {
        public XElement Convert(URL url)
        {
            XElement xmlUrl = new XElement("urlAddress",
                                    new XElement("host", new XAttribute("name", url.Host)),
                                    new XElement("uri", url.UrlPathSegments.Select(
                                                                (segment) => new XElement("segment", segment))),
                                    new XElement("parameters", url.Parameters.Select(
                                                                (pair) => new XElement("parameter", new XAttribute("key", pair.Key),
                                                                                                    new XAttribute("value", pair.Value))))
                                                                                                    );

            return xmlUrl;
        }
    }
}
