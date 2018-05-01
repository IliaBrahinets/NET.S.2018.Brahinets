using System;
using System.Collections.Generic;
using Task5.Solution.Converters;

namespace Task5.Solution
{
    public class Document
    {
        private List<DocumentPart> parts;

        public Document(IEnumerable<DocumentPart> parts)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }

            this.parts = new List<DocumentPart>(parts);
        }

        public string ToAnotherFromat(DocumentConverterProvider provider)
        {
            string output = string.Empty;

            foreach (DocumentPart part in this.parts)
            {
                output += $"{part.ToAnotherFormat(provider)}\n";
            }

            return output;
        }
    }
}
