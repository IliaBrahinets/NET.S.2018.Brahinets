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
        public static void Example()
        {
            BankAccountsFactory factory = new BankAccountsFactory();

            factory.CreateBaseBankAccount(1, new Client("Brahinets", "Ilia", "Andrevich"));
        }
    }
}
