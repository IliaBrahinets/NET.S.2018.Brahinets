using BankAccountLogic.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountLogic.Exceptions;

namespace BankAccountLogic
{
    public class BankAccountService
    {
        private readonly IBankAccountRepository repository;
        private readonly IBankAccountFactory factory;

        public BankAccountService(IBankAccountRepository repository, IBankAccountFactory factory)
        {
            this.repository = repository ?? throw new ArgumentNullException($"{nameof(repository)} is null");
            this.factory = factory ?? throw new ArgumentNullException($"{nameof(factory)} is null");
        }

        public void CreateAccount(AccountType accountType, int accountNumber, Client Owner, Decimal balance, int bonusPoints)
        {
            BankAccount account = null;
            switch (accountType)
            {
                case AccountType.BaseAccount:
                    account = factory.CreateBaseBankAccount(accountNumber, Owner, balance, bonusPoints);
                    break;
                case AccountType.GoldAccount:
                    account = factory.CreateGoldBankAccount(accountNumber, Owner, balance, bonusPoints);
                    break;
                case AccountType.SilverAccount:
                    account = factory.CreateSilverBankAccount(accountNumber, Owner, balance, bonusPoints);
                    break;
                case AccountType.PlatinumAccount:
                    account = factory.CreatePlatinumBankAccount(accountNumber, Owner, balance, bonusPoints);
                    break;
                default:
                    throw new ArgumentException($"AccountType:{accountType} doesn't exist");
            }

            try
            {
                repository.Create(account);
            }
            catch (Exception ex)
            {
                throw new DALException($"The error occured when creating the account", ex);
            }
        }

        public BankAccount GetByAccountNumber(int accountNumber)
        {
            return repository.GetByAccountNumber(accountNumber);
        }

        public void RemoveByAccountNumber(int accountNumber)
        {
            repository.RemoveByAccountNumber(accountNumber);
        }

        public IEnumerable<BankAccount> GetAllAccounts()
        {
            return repository.GetAll();
        }

    }
}
