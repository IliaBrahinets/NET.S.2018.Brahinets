using Ninject.Activation;
using System.Configuration;
using URLParser;

namespace Tests
{
    internal class FileLoggerCreator : Provider<FileLogger>
    {
        protected override FileLogger CreateInstance(IContext context)
        {
            string logPath = ConfigurationSettings.AppSettings["logPath"];

            return new FileLogger(logPath);
        }
    }
}