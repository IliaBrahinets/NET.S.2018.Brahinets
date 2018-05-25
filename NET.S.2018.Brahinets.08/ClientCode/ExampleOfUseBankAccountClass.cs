using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankAccountLogic;


namespace ClientCode
{
    public class ExampleOfUseBankAccountClass
    {
        public static void Main(string[] args)
        {
            BankAccountService service = new BankAccountService(new FakeRepository(), new BankAccountsFactory());

            service.CreateAccount(AccountType.BaseAccount, 1, new Client("Brahinets", "Ilia", "Andreevich"), 0, 0);

            Console.ReadKey();
        }
    }
}
