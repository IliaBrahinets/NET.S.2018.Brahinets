using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLogic
{
    public class BankAccountsFactory
    {
        public BankAccount CreateBaseBankAccount(int id, Client owner)
        {
            const int balanceImportancy = 1;
            const int replenishemntImportancy = 1;

            return new BankAccount(id, owner, balanceImportancy, replenishemntImportancy);
        }

        public BankAccount CreateSilverBankAccount(int id, Client owner)
        {
            const int balanceImportancy = 2;
            const int replenishemntImportancy = 2;

            return new BankAccount(id, owner, balanceImportancy, replenishemntImportancy);
        }

        public BankAccount CreateGoldBankAccount(int id, Client owner)
        {
            const int balanceImportancy = 3;
            const int replenishemntImportancy = 3;

            return new BankAccount(id, owner, balanceImportancy, replenishemntImportancy);
        }

        public BankAccount CreatePlatinumBankAccount(int id, Client owner)
        {
            const int balanceImportancy = 4;
            const int replenishemntImportancy = 4;

            return new BankAccount(id, owner, balanceImportancy, replenishemntImportancy);
        }
    }
}
