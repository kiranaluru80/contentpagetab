using System;
using System.Diagnostics.Contracts;
namespace ConEd.PAP.ExceptionalLogging
{
    public interface ILogManager
    {
		ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath]string callerFilePath = "");
	}
}
