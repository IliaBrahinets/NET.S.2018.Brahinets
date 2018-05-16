using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountLogic.BankAccounts;

namespace BankAccountLogic
{
    public class BankAccountsFactory:IBankAccountFactory
    {
        public BankAccount CreateBaseBankAccount(int accountNumber, Client owner, int balance = 0, int bonusPoints = 0)
        {
            return new BaseAccount(accountNumber, owner, balance, bonusPoints);
        }

        public BankAccount CreateSilverBankAccount(int accountNumber, Client owner, int balance = 0, int bonusPoints = 0)
        {
            return new SilverAccount(accountNumber, owner, balance, bonusPoints);
        }

        public BankAccount CreateGoldBankAccount(int accountNumber, Client owner, int balance = 0, int bonusPoints = 0)
        {
            return new GoldAccount(accountNumber, owner, balance, bonusPoints);
        }

        public BankAccount CreatePlatinumBankAccount(int accountNumber, Client owner, int balance = 0, int bonusPoints = 0)
        {
            return new PlatinumAccount(accountNumber, owner, balance, bonusPoints);
        }
    }
}
