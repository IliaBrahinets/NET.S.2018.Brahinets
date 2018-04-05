using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Logic;

[TestFixture]
public class BooksMediumFormatterTests
{
    private static List<TestCaseData> TestData { get; set; } = new List<TestCaseData>
        {
            new TestCaseData("M", new Book("1491987650", "Joseph Albahari&Ben Albahari",
                                "C# 7.0 in a Nutshell: The Definitive Reference", "O'Reilly Media", 2018, 1092, 44.99m))
                                .Returns("Joseph Albahari&Ben Albahari, C# 7.0 in a Nutshell: The Definitive Reference, O'Reilly Media, 2018"),
            new TestCaseData("S", new Book("1491987650", "Joseph Albahari&Ben Albahari",
                                "C# 7.0 in a Nutshell: The Definitive Reference", "O'Reilly Media", 2018, 1092, 44.99m))
                                .Returns("Joseph Albahari&Ben Albahari, C# 7.0 in a Nutshell: The Definitive Reference")
        };

    [TestCaseSource(nameof(TestData))]
    public string FormatMethod(string format, Book book)
    {
        format = "{0:" + format + "}";

        string actual = String.Format(new BooksMediumFormatter(), format, book);

        return actual;
    }
}

