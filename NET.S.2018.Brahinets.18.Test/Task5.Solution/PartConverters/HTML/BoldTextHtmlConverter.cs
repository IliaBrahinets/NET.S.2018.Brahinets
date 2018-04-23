using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.Solution;

namespace Task5.Solution.PartConverters.HTML
{
    public class BoldTextHtmlConverter : IDocumentPartConverter
    {
        public string Convert(DocumentPart elem)
        {
            BoldText target = (BoldText)elem;
            return "<b>" + target.Text + "</b>"; ;
        }
    }
}
