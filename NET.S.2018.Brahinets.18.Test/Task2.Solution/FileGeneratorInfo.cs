using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Solution
{
    public struct FileGeneratorInfo
    {
        public string WorkingDirectory { get; set; }
        public string FileExtension { get; set; }

        public FileGeneratorInfo(string workingDirectory, string fileExtension)
        {
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException($"{workingDirectory} is null");
            FileExtension = fileExtension ?? throw new ArgumentNullException($"{fileExtension} is null");
        }

    }
}
