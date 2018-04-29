using System;
using Xamarin.Forms;
using ConEd.PAP.iOS;
using ConEd.PAP.DependencyServices;
using ConEd.PAP.iOS.DependencyServices_iOS;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkDependency_iOS))]
namespace ConEd.PAP.iOS.DependencyServices_iOS
{
    public class NetworkDependency_iOS:INetworkDependency
    {
		public NetworkDependency_iOS()
		{
		}
		public string IsNetworkAvailable()
		{
			AppDelegate appDelegate = new AppDelegate();
			return appDelegate.updateStatus();
		}
    }
}
