using Ninject;
using Ninject.Activation;
using System.Configuration;
using URLParser;

namespace Tests
{
    internal class FileUrlProviderCreator : Provider<FileURLProvider>
    {
        protected override FileURLProvider CreateInstance(IContext context)
        {
            string filePath = ConfigurationSettings.AppSettings["readFrom"];

            return new FileURLProvider(filePath, context.Kernel.Get<IURLParser>(), context.Kernel.Get<ILogger>(),context.Kernel.Get<IURLValidator>());
        }
    }
}