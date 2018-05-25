using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountLogic.BankAccounts;

namespace BankAccountLogic
{
    public interface IBankAccountFactory
    {
        BankAccount CreateBaseBankAccount(int accountNumber, Client owner, Decimal balance, int bonusPoints);
        BankAccount CreateSilverBankAccount(int accountNumber, Client owner, Decimal balance, int bonusPoints);
        BankAccount CreateGoldBankAccount(int accountNumber, Client owner, Decimal balance, int bonusPoints);
        BankAccount CreatePlatinumBankAccount(int accountNumber, Client owner, Decimal balance, int bonusPoints);
    }
}
