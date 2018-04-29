using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP.Common.LoginHelper.NativeAppEmulator
{
    public static class NativeJsseConstants
    {
        static string tenant = constantfile.tenant;//ConfigurationManager.AppSettings["tenant"];
        static string aadInstance = constantfile.aadInstance;//ConfigurationManager.AppSettings["aadInstance"];
        static string nativeAppId = constantfile.nativeAppId;//ConfigurationManager.AppSettings["nativeAppId"];
        static Uri redirectURI = new Uri(constantfile.redirectURI);//new Uri(ConfigurationManager.AppSettings["redirectURI"]);
        static string webApiAppIdUri = constantfile.webApiAppIdUri;//ConfigurationManager.AppSettings["webApiAppIdUri"];
        private static string serviceBaseAddress = constantfile.serviceBaseAddress;//ConfigurationManager.AppSettings["serviceBaseAddress"];
        private static string authority = String.Format(CultureInfo.InvariantCulture, AadInstance, Tenant);
        
        public static string Authority
        {
            get
            {
                return authority;
            }

            set
            {
                authority = value;
            }
        }
        public static string Tenant
        {
            get
            {
                return tenant;
            }

            set
            {
                tenant = value;
            }
        }

        public static string AadInstance
        {
            get
            {
                return aadInstance;
            }

            set
            {
                aadInstance = value;
            }
        }

        public static string NativeAppId
        {
            get
            {
                return nativeAppId;
            }

            set
            {
                nativeAppId = value;
            }
        }

        public static Uri RedirectURI
        {
            get
            {
                return redirectURI;
            }

            set
            {
                redirectURI = value;
            }
        }

        public static string WebApiAppIdUri
        {
            get
            {
                return webApiAppIdUri;
            }

            set
            {
                webApiAppIdUri = value;
            }
        }

        public static string ServiceBaseAddress
        {
            get
            {
                return serviceBaseAddress;
            }

            set
            {
                serviceBaseAddress = value;
            }
        }        
    }
}
