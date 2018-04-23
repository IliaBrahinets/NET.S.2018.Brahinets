using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution.PartConverters.HTML
{
    public class HyperLinkHtmlConverter : IDocumentPartConverter
    {
        public string Convert(DocumentPart elem)
        {
            Hyperlink target = (Hyperlink)elem;
            return "<a href=\"" + target.Url + "\">" + target.Text + "</a>";
        }
    }
}
