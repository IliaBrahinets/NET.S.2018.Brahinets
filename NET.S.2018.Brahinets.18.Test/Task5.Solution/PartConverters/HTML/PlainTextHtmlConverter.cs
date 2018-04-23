using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution.PartConverters.HTML
{
    public class PlainTextHtmlConverter : IDocumentPartConverter
    {
        public string Convert(DocumentPart elem)
        {
            PlainText target = (PlainText)elem;
            return target.Text;
        }
    }
}
