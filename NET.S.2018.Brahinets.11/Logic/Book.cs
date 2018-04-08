using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable, IFormattable
    {
        public string ISBN { get; private set; }

        public string Author { get; private set; }

        public string Title { get; private set; }

        public string Publisher { get; private set; }

        public int PublishYear { get; private set; }

        public int NumberOfPages { get; private set; }

        public Decimal Price { get; private set; }

        public Book(string isbn, string author, string title, string publisher,
            int publishYear, int numberOfPages, Decimal price)
        {
            DataValidation(isbn, author, title, publisher, publishYear, numberOfPages, price);

            ISBN = isbn;
            Author = author;
            Title = title;
            Publisher = publisher;
            PublishYear = publishYear;
            NumberOfPages = numberOfPages;
            Price = price;
        }

        private void DataValidation(string isbn, string author, string title, string publisher,
            int publishYear, int numberOfPages, Decimal price)
        {
            if (!ISBNHelper.IsISBNValid(isbn))
            {
                throw new ArgumentException($"{nameof(isbn)} is not valid");
            }

            if (author == null)
            {
                throw new ArgumentNullException($"{nameof(author)} is null");
            }

            if (title == null)
            {
                throw new ArgumentNullException($"{nameof(title)} is null");
            }

            if (publisher == null)
            {
                throw new ArgumentNullException($"{nameof(publisher)} is null");
            }

            if (publishYear <= 0 || publishYear > 9999)
            {
                throw new ArgumentNullException($"{nameof(publishYear)} is not valid");
            }

            if (numberOfPages < 0)
            {
                throw new ArgumentException($"{nameof(numberOfPages)} is not valid");
            }

            if (price < 0)
            {
                throw new ArgumentException($"{nameof(price)} is not valid");
            }
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if(obj.GetType() == this.GetType())
            {
                return Equals(obj as Book);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            if (ISBN == other.ISBN)
            if (Equals(Author, other.Author))
            if (Equals(Title, other.Title))
            if (Equals(Publisher, other.Publisher))
            if (PublishYear == other.PublishYear)
            if (NumberOfPages == other.NumberOfPages)
            if (Price.Equals(other.Price))
                return true;

            return false;

        }

        public int CompareTo(Book other)
        {
            if(other == null)
            {
                return 1;
            }

            return (int)(Price - other.Price);
        }

        /// <summary>
        /// Returns a string representation of the book, using the specified format.
        /// </summary>
        /// <param name="format">Avaliable formats:
        ///                     "G" - general
        ///                     "S" - short
        ///                     You can enumerate the components of the book you want to get, separeted by ','.
        ///                     In the result the components values will also be separeted by ','.
        ///                     Components shortcuts:
        ///                     "ISBN" - Books ISBN number
        ///                     "AUT" - Books authors
        ///                     "TT" - Books Title
        ///                     "PB" - Books publisher
        ///                     "PBY" - Books publish year
        ///                     "PG" - Books number of pages
        ///                     "PR" - Books Price
        ///                     Example:
        ///                     The general format looks like this "ISBN,AUT,TT,PB,PBY,PG,PR".
        ///                     The short format "AUT,TT".
        ///                     </param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            string defaultFormat = "G";

            if (format == null)
            {
                format = defaultFormat;
            }

            if(formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            format = format.ToUpper();

            string generalFormat =
                "ISBN,AUT,TT,PB,PBY,PG,PR";
            string shortFormat =
                "AUT,TT";

            if (format == "G")
                format = generalFormat;

            if (format == "S")
                format = shortFormat;
                

            string[] fmtParts = format.Split(',');

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < fmtParts.Length; i++)
            {
                if (i != 0)
                {
                    result.Append(", ");
                }

                switch (fmtParts[i])
                {
                    case "ISBN":
                        result.Append(ISBN.ToString());
                        break;
                    case "AUT":
                        result.Append(Author.ToString(formatProvider));
                        break;
                    case "TT":
                        result.Append(Title.ToString(formatProvider));
                        break;
                    case "PB":
                        result.Append(Publisher.ToString(formatProvider));
                        break;
                    case "PBY":
                        result.Append(PublishYear.ToString(formatProvider));
                        break;
                    case "PG":
                        result.Append($"P.{NumberOfPages.ToString(formatProvider)}");
                        break;
                    case "PR":
                        result.Append(Price.ToString(formatProvider));
                        break;
                    default:
                        throw new FormatException($"{fmtParts[i]} format is not supported");
                }
            }

            return result.ToString();

        }

        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }

            if (obj.GetType() != this.GetType())
            {
                throw new ArgumentException($"{nameof(obj)}, type is not a {nameof(Book)}");
            }

            return CompareTo((Book)obj);
        }
    }
}
