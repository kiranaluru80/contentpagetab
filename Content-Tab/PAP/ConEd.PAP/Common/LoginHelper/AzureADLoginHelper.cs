//using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConEd.PAP.Common.LoginHelper.NativeAppEmulator
{
    public  class AzureADLoginHelper
    {
        public AuthenticationContext authContext = null;
        private AuthenticationResult result = null;
        public bool signedIn = false;
        private HttpClient httpclient = new HttpClient();

        //public IPlatformParameters PlatformParameters
        //{
        //    get; set;
        //}

        public AuthenticationResult Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        public AzureADLoginHelper()
        {
            //authContext = new AuthenticationContext(NativeJsseConstants.Authority, new FileCache());
            authContext = new AuthenticationContext(NativeJsseConstants.Authority);
        }

        /// <summary>
        /// 
        /// </summary>
        public async void SignIn()
        {
            if (signedIn == true)
            {
               // authContext.TokenCache.Clear();
                //Clear the cookies
                ClearCookies();
                signedIn = false;
                return;
            }
            try
            {
                //Microsoft.Identity.Client.AuthenticationResult ar = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                //IPlatformParameters pms = new PlatformParameters(PromptBehavior.Always);
                //var renderer = Platform.GetRenderer(page);
                //if (renderer == null)
                //{
                //    renderer = RendererFactory.GetRenderer(page);
                //    Platform.SetRenderer(page, renderer);
                //}
                //var viewController = renderer.ViewController;

                Result =  await authContext.AcquireTokenAsync(NativeJsseConstants.WebApiAppIdUri, NativeJsseConstants.NativeAppId, NativeJsseConstants.RedirectURI,null);
                /////await authContext.AcquireTokenAsync(,,,)
                signedIn = true;
            }
            catch (Exception ex)
            {
                //ServiceResult.Text = ex.ToString();
                throw ex;
            }
        }
        private void ClearCookies()
        {
            const int INTERNET_OPTION_END_BROWSER_SESSION = 42;
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);



    }
}
