using System;
using System.IO;

namespace Task2.Solution
{
    public class RandomBytesFileGenerator : FileGenerator
    {
        public RandomBytesFileGenerator(FileGeneratorInfo info) : base(info)
        {

        }

        protected override byte[] GenerateFileContent(int contentLength)
        {
            var random = new Random();

            var fileContent = new byte[contentLength];

            random.NextBytes(fileContent);

            return fileContent;
        }

    }
}