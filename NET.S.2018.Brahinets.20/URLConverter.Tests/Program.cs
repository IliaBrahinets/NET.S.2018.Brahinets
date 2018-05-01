using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ninject;
using URLParser;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new ConfigModule());

            XDocument result = kernel.Get<URLsToXMLService>().Convert();

            Console.WriteLine(result);

            string fileToSave = ConfigurationSettings.AppSettings["saveTo"];
            result.Save(fileToSave);

            Console.ReadKey();
            
        }
    }
}
