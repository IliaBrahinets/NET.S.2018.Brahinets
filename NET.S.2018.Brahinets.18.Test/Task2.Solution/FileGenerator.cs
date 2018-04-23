using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Solution
{
    public abstract class FileGenerator
    {
        protected FileGeneratorInfo generationInfo;

        public FileGenerator(FileGeneratorInfo info)
        {
            generationInfo = info;
        }

        public void GenerateFiles(int filesCount, int contentLength)
        {
            for (var i = 0; i < filesCount; ++i)
            {
                var generatedFileContent = this.GenerateFileContent(contentLength);

                var generatedFileName = $"{Guid.NewGuid()}{generationInfo.FileExtension}";

                this.WriteBytesToFile(generatedFileName, generatedFileContent);
            }
        }

        protected abstract byte[] GenerateFileContent(int contentLength);
        protected void WriteBytesToFile(string fileName, byte[] content)
        {
            if (!Directory.Exists(generationInfo.WorkingDirectory))
            {
                Directory.CreateDirectory(generationInfo.WorkingDirectory);
            }

            File.WriteAllBytes($"{generationInfo.WorkingDirectory}//{fileName}", content);
        }
    }
}
