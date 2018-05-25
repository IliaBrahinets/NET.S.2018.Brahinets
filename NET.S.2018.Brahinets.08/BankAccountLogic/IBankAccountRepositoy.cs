using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountLogic.BankAccounts;

namespace BankAccountLogic
{
    public interface IBankAccountRepository:IRepository<BankAccount>
    {
        BankAccount GetByAccountNumber(int accountNumber);
        void RemoveByAccountNumber(int accountNumber);
    }
}
