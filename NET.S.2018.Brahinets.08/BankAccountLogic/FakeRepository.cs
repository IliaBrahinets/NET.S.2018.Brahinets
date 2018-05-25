using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountLogic.BankAccounts;

namespace BankAccountLogic
{
    public class FakeRepository : IBankAccountRepository
    {
        public void Create(BankAccount entity)
        {
            Console.WriteLine("Was created");
        }

        public void Delete(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankAccount> GetAll()
        {
            throw new NotImplementedException();
        }

        public BankAccount GetByAccountNumber(int accountNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveByAccountNumber(int accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
