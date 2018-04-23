using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task5.Solution.PartConverters;

namespace Task5.Solution.Converters
{
    public abstract class DocumentConverterProvider
    {
        protected Dictionary<Type, IDocumentPartConverter> Converters { get; set; }

        public DocumentConverterProvider()
        {
            Converters = new Dictionary<Type, IDocumentPartConverter>();
        }

        public abstract string Convert(DocumentPart elem);
    }
}
