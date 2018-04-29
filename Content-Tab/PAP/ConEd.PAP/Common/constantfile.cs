using System;

using Xamarin.Forms;

namespace ConEd.PAP.Common
{
    public class constantfile
    {

        //public static string GetPolicyUrl = "api/policies/ByType/0?lastSyncDate=2017-10-21";//CEHSP
        public static string GetPolicyUrl = "api/policies/ByType/0";//CEHSP
        public static string GetDocumentUrl = "http://ehsdev1:83/api/policies/download";
        public static string GetDocumentFileUrl = "api/policies/DownloadFile";
        //public static string BaseUrl = "http://ehsdev1:83/";

        public static string AzureSaSKeyName = "RootManageSharedAccessKey";
        public static string AzureSaSKeyValue = "wOz3N2jW/bqKws1Vbbl110f7YrgfVjG9hhN8PCobIJc=";
        //public static string AzurebaseUrl = "https://jsse-relay-dev.servicebus.windows.net/";
        public static string BaseUrl = "https://ehs-apps-dev.servicebus.windows.net/";


        //login azure authentication
        public static string tenant = "consolidatededison.onmicrosoft.com";
        public static string aadInstance = "https://login.microsoftonline.com/{0}";
        public static string nativeAppId = "450d733f-eb96-4502-8ee2-58fdd1a65943";
        public static string redirectURI = "https://jsse-dev.azurewebsites.net/.auth/login/done";
        public static string webApiAppIdUri = "https://jsse-dev.azurewebsites.net/";
        public static string serviceBaseAddress = "https://jsse-dev.azurewebsites.net/";
        

    }
}

