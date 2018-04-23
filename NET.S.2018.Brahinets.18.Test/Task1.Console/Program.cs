using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Solution;
using Task1.Solution.Persistance;
using Task1.Solution.Conditions;

namespace Task1.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository repository = new FakeRepository();
            IPasswordCondition[] conditions =
            {
                new EmptyStringConditions(),
                new InvalidCharacterCondition(),
                new LengthCondition()
            };
            var service = new PasswordCheckerService(repository,conditions);

            var password = "ads222Sfsdf4df";
            System.Console.WriteLine(service.VerifyPassword(password));

            password = "";
            System.Console.WriteLine(service.VerifyPassword(password));
            System.Console.ReadKey();

        }
    }
}
