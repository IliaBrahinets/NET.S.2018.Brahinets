using System;
using System.Linq;

namespace Task1
{
    public class PasswordCheckerService
    {
        private SqlRepository repository = new SqlRepository();

        public Tuple<bool, string> VerifyPassword(string password)
        {
            if (password == null)
                throw new ArgumentException($"{password} is null arg");

            if (password == string.Empty)
                return Tuple.Create(false, $"{password} is empty ");

        

            // check if password conatins at least one alphabetical character 
            if (!password.Any(char.IsLetter))
                return Tuple.Create(false, $"{password} hasn't alphanumerical chars");

            // check if password conatins at least one digit character 
            if (!password.Any(char.IsNumber))
                return Tuple.Create(false, $"{password} hasn't digits");

            repository.Create(password);

            return Tuple.Create(true, "Password is Ok. User was created");
        }
    }
}
