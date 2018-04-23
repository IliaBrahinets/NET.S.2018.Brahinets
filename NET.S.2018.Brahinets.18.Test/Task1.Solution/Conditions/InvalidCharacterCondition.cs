using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Solution.Conditions
{
    public class InvalidCharacterCondition : IPasswordCondition
    {
        public Tuple<bool, string> IsValid(string password)
        {
            // check if password conatins at least one alphabetical character 
            if (!password.Any(char.IsLetter))
                return Tuple.Create(false, $"{password} hasn't alphanumerical chars");

            // check if password conatins at least one digit character 
            if (!password.Any(char.IsNumber))
                return Tuple.Create(false, $"{password} hasn't digits");

            return Tuple.Create(true, "");
        }
    }
}
