using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    internal class ISBNHelper
    {
        public static bool IsISBNValid(string isbn)
        {
            string isbnPattern = @"^(?:ISBN(?:-1[03])?:?\ )?(?=[0-9]{10}$|(?=(?:[0-9]+[-\ ]){3})[-\ 0-9]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)(?:97[89][-\ ]?)?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$";

            return Regex.IsMatch(isbn, isbnPattern);
        }
    }
}
