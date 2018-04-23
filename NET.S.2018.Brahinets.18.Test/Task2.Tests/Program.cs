using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Solution;

namespace Task2.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            FileGeneratorInfo info = new FileGeneratorInfo(@"C:\Generated",".txt");

            var bytesGenerator = new RandomBytesFileGenerator(info);

            bytesGenerator.GenerateFiles(1, 10);

            var charsGenerator = new RandomCharsFileGenerator(info);

            charsGenerator.GenerateFiles(1, 10);


        }
    }
}
