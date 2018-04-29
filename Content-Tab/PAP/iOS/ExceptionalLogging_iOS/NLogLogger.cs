using System;
using NLog;
using ConEd.PAP.iOS;
using Xamarin.Forms;
using ConEd.PAP.iOS.ExceptionalLogging_iOS;

[assembly: Dependency(typeof(NLogLogger))]
namespace ConEd.PAP.iOS.ExceptionalLogging_iOS
{
    public class NLogLogger:ConEd.PAP.ExceptionalLogging.ILogger
    {
		private Logger log;

		public NLogLogger(Logger log)
		{
			this.log = log;
		}

		public void Debug(string text, params object[] args)
		{
			log.Debug(text, args);
		}

		public void Error(string text, params object[] args)
		{
			log.Error(text, args);
		}

		public void Fatal(string text, params object[] args)
		{
			log.Fatal(text, args);
		}

		public void Info(string text, params object[] args)
		{
			log.Info(text, args);
		}

		public void Trace(string text, params object[] args)
		{
			log.Trace(text, args);
		}

		public void Warn(string text, params object[] args)
		{
			log.Warn(text, args);
		}
    }
}
