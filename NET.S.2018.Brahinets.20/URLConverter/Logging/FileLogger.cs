using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class FileLogger : ILogger
    {
        public string Path { get; set; }

        public FileLogger(string path)
        {
            Path = path ?? throw new ArgumentNullException($"{path} is null");
        }

        public void Log(string message)
        {
            try
            {

                CheckExistanceIfNotCreate();

                using (var file = File.AppendText(Path))
                {
                    file.WriteLine(message);
                }

            }
            catch
            {
                //i don't know how else to handle the exceptions during the logging
            }
        }

        private void CheckExistanceIfNotCreate()
        {
            if (!File.Exists(Path))
            {
                File.Create(Path).Dispose();
            }
        }
    }
}
