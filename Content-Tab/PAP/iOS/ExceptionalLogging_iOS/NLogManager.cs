using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using Foundation;
using ConEd.PAP.iOS;
using ConEd.PAP.ExceptionalLogging;
using Xamarin.Forms;
using ConEd.PAP.iOS.ExceptionalLogging_iOS;

[assembly: Dependency(typeof(NLogManager))]
namespace ConEd.PAP.iOS.ExceptionalLogging_iOS
{
    public class NLogManager:ConEd.PAP.ExceptionalLogging.ILogManager
    {
        public NLogManager()
        {
			var config = new LoggingConfiguration();
			var consoleTarget = new ConsoleTarget();
			config.AddTarget("console", consoleTarget);

			var consoleRule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
			config.LoggingRules.Add(consoleRule);

			var fileTarget = new FileTarget();
			string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			fileTarget.FileName = Path.Combine(folder, "Log.txt");
			config.AddTarget("file", fileTarget);
			var fileRule = new LoggingRule("*", LogLevel.Warn, fileTarget);
			config.LoggingRules.Add(fileRule);
			LogManager.Configuration = config;
        }
        public ConEd.PAP.ExceptionalLogging.ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "")
		{
			string fileName = callerFilePath;
			if (fileName.Contains("/"))
			{
				fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
			}
			var logger = LogManager.GetLogger(fileName);
			return new NLogLogger(logger);
		}
    }
}
