using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task5.Solution.PartConverters;
using Task5.Solution.PartConverters.HTML;

namespace Task5.Solution.Converters
{
    public class HtmlConverter : DocumentConverterProvider
    {
        public HtmlConverter()
        {
            Converters.Add(typeof(BoldText), new BoldTextHtmlConverter());
            Converters.Add(typeof(Hyperlink), new HyperLinkHtmlConverter());
            Converters.Add(typeof(PlainText), new PlainTextHtmlConverter());
        }

        public override string Convert(DocumentPart elem)
        {
            IDocumentPartConverter converter;

            if (Converters.TryGetValue(elem.GetType(), out converter))
            {
                return converter.Convert(elem);
            }

            throw new InvalidOperationException($"can't find a converter for the {nameof(elem)}");
        }
    }
}
