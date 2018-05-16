using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLogic.BankAccounts
{
    class GoldAccount:BankAccount
    {
        public GoldAccount(int accountNumber, Client owner,
            Decimal balance = 0m, int bonusPoints = 0) : base(accountNumber, owner, 10, 10, balance, bonusPoints)
        {

        }

        protected override int CalculateDepositBonusPointsUpdate(decimal amount)
        {
            return DepositBonusValue;
        }

        protected override int CalculateWithdrawBonusPointsUpdate(decimal amount)
        {
            return WithdrawBonusValue;
        }
    }
}
