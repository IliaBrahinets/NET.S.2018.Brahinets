using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLogic
{
    public class BankAccount
    {
        public int Id { get; private set; }
        public Client Owner { get; private set; }
        public Decimal Balance { get; private set; }
        public int BonusPoints { get; private set; }

        protected int BalanceImportancy { get; }
        protected int ReplenishemntImportancy { get; }

        /// <param name="id">An entity, that identificated account.</param>
        /// <param name="owner">An onwer of the bank account.</param>
        /// <param name="balanceImportancy">This value depend on a gradation of the account.</param>
        /// <param name="replenishemntImportancy">This value depend on a gradation of the account.</param>
        /// <param name="balance">An initial balance.</param>
        /// <param name="bonusPoints">An inital bonus points.</param>
        /// <exception cref="ArgumentNullException">Thrown when owner is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when balanceImportancy or replenishemntImportancy or balance or bonusPoints
        ///                                               is less than zero.</exception>
        public BankAccount(int id, Client owner, int balanceImportancy, int replenishemntImportancy,
            Decimal balance = 0m, int bonusPoints = 0)
        {
            ConstructorDataValidate(owner, balanceImportancy, replenishemntImportancy, balance, bonusPoints);

            Id = id;
            Owner = new Client(owner);
            Balance = balance;
            BonusPoints = bonusPoints;
            BalanceImportancy = balanceImportancy;
            ReplenishemntImportancy = replenishemntImportancy;
        }

        private void ConstructorDataValidate(Client owner, int balanceImportancy,
            int replenishemntImportancy, Decimal balance, int bonusPoints)
        {
            if (owner == null)
            {
                throw new ArgumentNullException($"{nameof(owner)} can't be null!");
            }

            if (balanceImportancy < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(balanceImportancy)} must be >= 0");
            }

            if (replenishemntImportancy < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(replenishemntImportancy)} must be >= 0");
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

        /// <summary>
        /// Replenishing an account.
        /// </summary>
        /// <param name="amount">This is an amount to replenish on.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the new balance exceeds Decimal.MaxValue.</exception>
        public virtual void Replenish(Decimal amount)
        {
            ReplenishAndDebitValidation(amount);

            ReplenishsBonusPoints(amount);

            if((Decimal.MaxValue - Balance) < amount)
            {
                throw new InvalidOperationException($"new {Balance} is exceeded Decimal.MaxValue");
            }

            Balance += amount;
        }

        /// <summary>
        /// Debiting an account.
        /// </summary>
        /// <param name="amount">This is an amount to debit on.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the new balance exceeds Decimal.MaxValue.</exception>
        public virtual void Debit(Decimal amount)
        {
            ReplenishAndDebitValidation(amount);

            DebitsBonusPoints(amount);

            if (Balance < amount)
            {
                throw new InvalidOperationException($"new {Balance} is less than zero");
            }

            Balance -= amount;
        }

        private void ReplenishAndDebitValidation(Decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(amount)} must be >= 0");
            }
        }

        protected virtual void ReplenishsBonusPoints(Decimal amount)
        {
            BonusPoints += ReplenishemntImportancy;
        }

        protected virtual void DebitsBonusPoints(Decimal amount)
        {
            BonusPoints -= BalanceImportancy;
        }
    }
}
