using Task5.Solution.Converters;

namespace Task5.Solution
{
    public abstract class DocumentPart
    {
        public string Text { get; set; }

        public string ToAnotherFormat(DocumentConverterProvider provider)
        {
            return provider.Convert(this);
        }
    }
}
