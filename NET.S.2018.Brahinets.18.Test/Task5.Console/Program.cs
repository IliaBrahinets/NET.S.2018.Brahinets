using System.Collections.Generic;
using System;
using Task5;
using Task5.Solution.Converters;
using Task5.Solution;

namespace Task5.Console
{

    class Program
    {
        static void Main(string[] args)
        {
            List<DocumentPart> parts = new List<DocumentPart>
                {
                    new PlainText {Text = "Some plain text"},
                    new Hyperlink {Text = "google.com", Url = "https://www.google.by/"},
                    new BoldText {Text = "Some bold text"}
                };

            Document document = new Document(parts);

            DocumentConverterProvider htmlProvider = new HtmlConverter();

            System.Console.WriteLine(document.ToAnotherFromat(htmlProvider));

            System.Console.ReadKey();
        }
    }
}
