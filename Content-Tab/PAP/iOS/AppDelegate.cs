using System;
using System.Collections.Generic;
using System.Linq;
using NControl.iOS;
using Prism.Unity;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Foundation;
using UIKit;
using ConEd.PAP;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using ConEd.PAP.Common.LoginHelper.NativeAppEmulator;

//using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Identity.Client;
using HockeyApp.iOS;

namespace ConEd.PAP.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        //login azure authentication
        public static string tenant = "consolidatededison.onmicrosoft.com";
        public static string aadInstance = "https://login.microsoftonline.com/{0}";
        public static string nativeAppId = "450d733f-eb96-4502-8ee2-58fdd1a65943";
        public static string redirectURI = "https://jsse-dev.azurewebsites.net/.auth/login/done";
        public static string webApiAppIdUri = "https://jsse-dev.azurewebsites.net/";
        public static string serviceBaseAddress = "https://jsse-dev.azurewebsites.net/";
        private static string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
        //  public  AuthenticationContext authContext = null;
        //private AuthenticationResult result = null;
      
        //public AuthenticationResult authResult {
        //    get { return result; }
        //    set {
        //        result = value;
        //    } }
        //public override void OnActivated(UIApplication uiApplication)
        //{
        //    base.OnActivated(uiApplication);
        

        //    //GetResult();

          
        //}

        //public async void GetResult()
        //{
        //    AuthenticationContext authContext = new AuthenticationContext(authority);
        //    var pvc = GetViewControllerCurrent();

        //    var ptParams = new PlatformParameters(pvc,false, PromptBehavior.Always);
         
        //    authResult = await authContext.AcquireTokenAsync(NativeJsseConstants.WebApiAppIdUri, NativeJsseConstants.NativeAppId, NativeJsseConstants.RedirectURI, ptParams);
              
            
        //}
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            NControlViewRenderer.Init();
            string dbPath = FileAccessHelper.GetLocalFilePath("PAP.db3");

            //LoadApplication(new App(new iOSInitializer()));
            //AzureADLoginHelper aad = new AzureADLoginHelper();
            //aad.SignIn();       
               
            LoadApplication(new App(dbPath, new SQLitePlatformIOS(), new iOSInitializer()));
			Reachability.ReachabilityChanged += c_ReachabilityChanged;
			//var manager = BITHockeyManager.SharedHockeyManager;
			//manager.Configure("$Your_App_Id");
			//manager.StartManager();
			//manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds
															  
            //App.PCA.RedirectUri = "https://jsse-dev.azurewebsites.net/.auth/login/done";
			return base.FinishedLaunching(app, options);

        }
		public string updateStatus()
		{
			string updateStatusValue = "";

			NetworkStatus remoteHostStatus = Reachability.InternetConnectionStatus();

			switch (remoteHostStatus)
			{
				case NetworkStatus.NotReachable:
					//Debug.WriteLine ("Not Reachable Appdelegate");
					updateStatusValue = "Not Rechable";
					break;
				case NetworkStatus.ReachableViaCarrierDataNetwork:
					//Debug.WriteLine ("Reachable Appdelegate");
					updateStatusValue = "Available";

					break;
				case NetworkStatus.ReachableViaWiFiNetwork:
					//Debug.WriteLine ("Reachable Appdelegate");
					updateStatusValue = "Available";

					break;
			}
			return updateStatusValue;
		}

		static void c_ReachabilityChanged(object sender, EventArgs e)
		{
			string updateStatusValue = "";

			NetworkStatus remoteHostStatus = Reachability.InternetConnectionStatus();

			switch (remoteHostStatus)
			{
				case NetworkStatus.NotReachable:
					//Debug.WriteLine ("Not Reachable Appdelegate");
					updateStatusValue = "Not Rechable";
					UIAlertView alert = new UIAlertView()
					{
						Title = "alert title",
						Message = "Network Not Rechable"
					};
					alert.AddButton("OK");
					alert.Show();

					break;
				case NetworkStatus.ReachableViaCarrierDataNetwork:
					//Debug.WriteLine ("Reachable Appdelegate");
					updateStatusValue = "Available";
					UIAlertView alert1 = new UIAlertView()
					{
						Title = "alert title",
						Message = "Network Rechable"
					};
					alert1.AddButton("OK");
					alert1.Show();
					//App.networkCallBack("Network Rechable");
					break;
				case NetworkStatus.ReachableViaWiFiNetwork:
					//Debug.WriteLine ("Reachable Appdelegate");
					updateStatusValue = "Available";
					UIAlertView alert2 = new UIAlertView()
					{
						Title = "alert title",
						Message = "Network Rechable"
					};
					alert2.AddButton("OK");
					alert2.Show();
					//App.networkCallBack("Network Rechable");
					break;
			}
		}

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return true;
        }

        public override UIViewController GetViewController(UIApplication application, string[] restorationIdentifierComponents, NSCoder coder)
        {
            return base.GetViewController(application, restorationIdentifierComponents, coder);
   

        }
        public UIViewController GetViewControllerCurrent()
        {

            var window = UIApplication.SharedApplication.Windows.First();
            //var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
            return vc;
        }
        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IUnityContainer container)
            {

            }
        }
    }
}
