using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLogic
{
    public class Client
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{SurName} {Name} {LastName}";
            }
        }

        public Client(string surName, string name, string lastName)
        {
            SurName = surName;
            Name = name;
            LastName = lastName;
        }

        public Client(Client client)
        {
            SurName = client.SurName;
            Name = client.Name;
            LastName = client.LastName;
        }

        public override string ToString()
        {
            return FullName;

    }
}
