using Xamarin.Forms;
using ConEd.PAP.Views;
using ConEd.PAP.Data;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using SQLite.Net.Interop;
using System;
using ConEd.PAP.ViewModels;
using ConEd.PAP.Common.LoginHelper.NativeAppEmulator;
using Microsoft.Identity.Client;
using ConEd.PAP.ExceptionalLogging;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ConEd.PAP
{
    public partial class App : PrismApplication
    {
        public static PublicClientApplication PCA = null;
        public static string ClientID = "450d733f-eb96-4502-8ee2-58fdd1a65943";
        public static string[] Scopes = { "User.Read" };
        public static string Username = string.Empty;
        public static UIParent UiParent = null;
        public static PoliciesRepository PoliciesRepo { get; private set; }
		private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();


		//public string DocTypeName { get; set; }
		public App(string dbPath, ISQLitePlatform sqlitePlatform, IPlatformInitializer initializer = null) : base(initializer)
        {
            //set database path first, then retrieve main page
            PoliciesRepo = new PoliciesRepository(sqlitePlatform, dbPath);
            //CommonViewModel cvm = new CommonViewModel();
            //cvm.SetOnlineDocumentsToLocal();

            //call azure AD
            //AzureADLoginHelper aad = new AzureADLoginHelper();
            //aad.SignIn();
            PCA = new PublicClientApplication(ClientID);
            //MainPage = new NavigationPage(new MainPage());
        }
        //public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
		{
			InitializeComponent();
			logger.Info("App Start");
			//NavigationService.NavigateAsync("NavigationPage/LoginPage");
			//NavigationService.NavigateAsync("NavigationPage/DocumentTypes");
			MainPage = new NavigationPage(new DocumentTypes(""));
		}

		protected override void RegisterTypes()
		{
            Container.RegisterTypeForNavigation<NavigationPage>();
          
            //Container.RegisterTypeForNavigation<DocumentTypes>();
            Container.RegisterTypeForNavigation<DocumentDetails>();
            Container.RegisterTypeForNavigation<DDE>();
            Container.RegisterTypeForNavigation<ViewDocument>();
            Container.RegisterTypeForNavigation<GenericLanding>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
