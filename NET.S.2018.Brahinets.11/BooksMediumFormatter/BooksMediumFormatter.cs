using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

public class BooksMediumFormatter : IFormatProvider, ICustomFormatter
{
    public object GetFormat(Type formatType)
    {
        if (formatType == typeof(ICustomFormatter))
            return this;
        else
            return null;
    }

    public string Format(string format, object arg, IFormatProvider formatProvider)
    {
        if (!(arg is Book))
        {
            try
            {
                return HandleOtherFormats(format, arg);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"{format} is not supported", ex);
            }
        }

        Book book = arg as Book;

        if (String.IsNullOrEmpty(format))
        {
            return book.ToString();
        }

        format = format.ToUpper();

        string mediumFormat = "AUT,TT,PB,PBY";

        string result = null;

        switch (format)
        {
            case "M":
                result = book.ToString(mediumFormat, null);
                break;
            default:
                try
                {
                    HandleOtherFormats(format, arg);
                }
                catch (FormatException ex)
                {
                    throw new FormatException($"{format} is not supported", ex);
                }
                break;
        }

        return result;
    }

    private string HandleOtherFormats(string format, object arg)
    {
        if (arg is IFormattable)
            return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
        else if (arg != null)
            return arg.ToString();
        else
            return String.Empty;
    }
}
