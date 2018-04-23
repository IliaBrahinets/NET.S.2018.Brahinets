using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Solution.Persistance;

namespace Task1.Solution
{
    public class PasswordCheckerService
    {         
        private IRepository repository;

        private IEnumerable<IPasswordCondition> conditions;

        public PasswordCheckerService(IRepository repository, IEnumerable<IPasswordCondition> conditions)
        {
            this.repository = repository ?? throw new ArgumentNullException($"{nameof(repository)} is null");
            this.conditions = conditions ?? throw new ArgumentNullException($"{nameof(conditions)} is null");
        }

        public Tuple<bool, string> VerifyPassword(string password)
        {
            if (password == null)
                throw new ArgumentException($"{password} is null arg");

            var result = conditions.Select(x => x.IsValid(password));

            if (result.All(x => x.Item1))
            {
                repository.Create(password);
                return Tuple.Create(true, "Password is Ok. User was created");
            }

            return result.First(x => !x.Item1);


        }
    }
}
