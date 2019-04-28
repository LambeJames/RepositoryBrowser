using RepositoryBrowser.Interfaces.Services.Logging;
using System;
using System.IO;
using System.Reflection;

namespace RepositoryBrowser.Services.Logging
{
    public class Logger : ILogger
    {
        private readonly string path;

        public Logger()
        {
            path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public void LogMessage(string message)
        {
            using (StreamWriter writer = File.AppendText(path + "\\" + "log.txt"))
            {
                writer.Write("\r\nLog Entry : ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                writer.WriteLine("  :");
                writer.WriteLine("  :{0}", message);
                writer.WriteLine("-------------------------------");
            }
        }
    }
}
