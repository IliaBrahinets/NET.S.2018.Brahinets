using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLogic.BankAccounts
{
    public abstract class BankAccount
    {
        #region fields
        #endregion

        #region properties 
        public int AccountNumber { get; private set; }
        public Client Owner { get; private set; }
        public Decimal Balance { get; private set; }
        public int BonusPoints { get; private set; }

        protected int WithdrawBonusValue { get; }
        protected int DepositBonusValue { get; }
        #endregion

        #region constructors
        /// <param name="AccountNumber">An entity, that identificated account.</param>
        /// <param name="owner">An onwer of the bank account.</param>
        /// <param name="withdrawBonusValue">This value depend on a gradation of the account, used to update bonus point after a withdraw.</param>
        /// <param name="depositBonusValue">This value depend on a gradation of the account, used to update bonus point after a deposit.</param>
        /// <param name="balance">An initial balance.</param>
        /// <param name="bonusPoints">An inital bonus points.</param>
        /// <exception cref="ArgumentNullException">Thrown when owner is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="withdrawBonusValue"/> or <paramref name="depositBonusValue"/> or balance or bonusPoints
        ///                                               is less than zero.</exception>
        public BankAccount(int accountNumber, Client owner, int withdrawBonusValue, int depositBonusValue,
            Decimal balance = 0m, int bonusPoints = 0)
        {
            ConstructorDataValidate(owner, withdrawBonusValue, depositBonusValue, balance, bonusPoints);

            AccountNumber = accountNumber;
            Owner = new Client(owner);
            Balance = balance;
            BonusPoints = bonusPoints;
            WithdrawBonusValue = withdrawBonusValue;
            DepositBonusValue = depositBonusValue;
        }
        #endregion

        #region methods
        /// <summary>
        /// Deposit into an account.
        /// </summary>
        /// <param name="amount">This is an amount of a deposit.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the new balance exceeds Decimal.MaxValue.</exception>
        public void Deposit(Decimal amount)
        {
            DepositAndWithdrawValidation(amount);

            if((Decimal.MaxValue - Balance) < amount)
            {
                throw new InvalidOperationException($"new {Balance} is exceeded Decimal.MaxValue");
            }

            Balance += amount;
            BonusPoints -= CalculateDepositBonusPointsUpdate(amount);
        }       

        /// <summary>
        /// Withdraw cash from the account.
        /// </summary>
        /// <param name="amount">This is an amount of cash to withdraw.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the new balance is less than zero.</exception>
        public void Withdraw(Decimal amount)
        {
            DepositAndWithdrawValidation(amount);

            if (Balance < amount)
            {
                throw new InvalidOperationException($"new {Balance} is less than zero");
            }

            Balance -= amount;
            BonusPoints -= CalculateWithdrawBonusPointsUpdate(amount);
        }

        protected abstract int CalculateWithdrawBonusPointsUpdate(Decimal amount);

        protected abstract int CalculateDepositBonusPointsUpdate(Decimal amount);

        private void ConstructorDataValidate(Client owner, int withdrawBonusValue,
           int depositBonusValue, Decimal balance, int bonusPoints)
        {
            if (owner == null)
            {
                throw new ArgumentNullException($"{nameof(owner)} can't be null!");
            }

            if (withdrawBonusValue < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(withdrawBonusValue)} must be >= 0");
            }

            if (depositBonusValue < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(depositBonusValue)} must be >= 0");
            }

            if (balance < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(balance)} must be >= 0");
            }

            if (bonusPoints < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(bonusPoints)} must be >= 0");
            }
        }

        private void DepositAndWithdrawValidation(Decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(amount)} must be >= 0");
            }
        }

        #region objectOverrides

        public override string ToString()
        {
            return $"AccountNumber:{AccountNumber}, AccountType:{this.GetType().Name}, Owner:{Owner}, Balance:{Balance}, BonusPoints:{BonusPoints}";
        }

        #endregion
        #endregion
    }
}
