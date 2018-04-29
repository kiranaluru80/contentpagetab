//using EHS.JSSE.API.Entity;
using ConEd.PAP.Common;
using ConEd.PAP.Models;
using Newtonsoft.Json;
using PCLStorage;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;
//using System.Windows.Forms;

namespace ConEd.PAP.WebAPI
{
    public class ServiceBusRelay
    {
        public const string ServiceBusNamespace = "ehs-apps-dev";
        public const string servicePath = "Services/JsseService.svc";
        public const string BaseServiceBusAddress = "https://" + ServiceBusNamespace + ".servicebus.windows.net/";
        //public static string jsseAzureSBURL = ConfigurationManager.AppSettings["jsseAzureSBRelayURL"];
        public static string jsseAzureSBURL = constantfile.BaseUrl;
        public static string jsseAzureSASKeyName = constantfile.AzureSaSKeyName;
        public static string jsseAzureSASKeyValue = constantfile.AzureSaSKeyValue;


		//public static string jsseAzureSASKeyName = ConfigurationManager.AppSettings["jsseAzureSASKeyName"];
        //public static string jsseAzureSASKeyValue = ConfigurationManager.AppSettings["jsseAzureSASKeyValue"];
        //public static string regionsAPIPath = "api/jsse/Regions";
        //public static string updateJSSETempAPIPath = "api/jsse/updateJSSETemp";
        private static string createToken(string sasKeyName, string sasKeyValue)
        {
            TimeSpan sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var week = 60 * 60 * 24 * 7;
            var expiry = Convert.ToString((int)sinceEpoch.TotalSeconds + week);
            //string stringToSign = WebUtility.UrlEncode(jsseAzureSBURL) + "\n" + expiry;
            string stringToSign = WebUtility.UrlEncode(constantfile.BaseUrl) + "\n" + expiry;
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(sasKeyValue));
            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            var sasToken = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}", WebUtility.UrlEncode(BaseServiceBusAddress), WebUtility.UrlEncode(signature), expiry, sasKeyName);
            return sasToken;
        }

        public static async Task<HttpResponseMessage> GetAsyncAPI(string apiPath)
        {
            using (HttpClient client = CreateHttpClient())
            {
                client.BaseAddress = new Uri(jsseAzureSBURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var token = GetSASToken(sasKeyName, sasKey);
                //client.DefaultRequestHeaders.Add("Authorization", token);
                // List<Region> regions = null;
                //HttpContent content = new StringContent(JsonConvert.SerializeObject(jsse), Encoding.UTF8);
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //Region[] regions = null;
                var path = jsseAzureSBURL + apiPath;
                HttpResponseMessage response = await client.GetAsync(path);
                //    .ContinueWith((taskwithresponse) =>
                //{
                //    var response = taskwithresponse.Result;
                //    var jsonString = response.Content.ReadAsStringAsync();
                //    jsonString.Wait();
                //    regions = JsonConvert.DeserializeObject<Region[]>(jsonString.Result);

                //});
                //res.Wait();
                //MessageBox.Show("Regions fetched from azure:" + regions.Length);
                //Region[] regions = JsonConvert.DeserializeObject<Region[]>(res.Content.ToString());
                return response;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiPath"></param>
        /// <param name="requestContent">Json seriealized string conetnt for creating HttpContent object</param>
        /// <returns></returns>
        //public static async Task<HttpResponseMessage> PostAsyncAPI(string apiPath, JSSEMain requestContent)
        //{
        //    using (HttpClient client = CreateHttpClient())
        //    {
        //        client.BaseAddress = new Uri(jsseAzureSBURL);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //var token = GetSASToken(sasKeyName, sasKey);
        //        //client.DefaultRequestHeaders.Add("Authorization", token);
        //        // List<Region> regions = null;
        //        //HttpContent content = new StringContent(JsonConvert.SerializeObject(jsse), Encoding.UTF8);
        //        //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        //Region[] regions = null;
        //        var path = jsseAzureSBURL + apiPath;
        //       // HttpContent reqContent = new StringContent(requestContent, Encoding.UTF8, "application/json");
        //        //using (var formData = new MultipartFormDataContent())
        //        //{
        //        //    //add content to form data
        //        //    formData.Add(new StringContent(requestContent), "Content");

        //            HttpResponseMessage response = await client.PostAsJsonAsync(path, requestContent);

        //            //    .ContinueWith((taskwithresponse) =>
        //            //{
        //            //    var response = taskwithresponse.Result;
        //            //    var jsonString = response.Content.ReadAsStringAsync();
        //            //    jsonString.Wait();
        //            //    regions = JsonConvert.DeserializeObject<Region[]>(jsonString.Result);

        //            //});
        //            //res.Wait();
        //            //MessageBox.Show("Regions fetched from azure:" + regions.Length);
        //            //Region[] regions = JsonConvert.DeserializeObject<Region[]>(res.Content.ToString());
        //            return response;
        //        //}
        //    }
        //}

        /// <summary>
        /// Working code Sync
        /// </summary>
        /// <param name="apiPath"></param>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public static IRestResponse PostREST(string apiPath, object requestContent)
        {
            RestClient client = CreateRestClient();

            client.BaseUrl = new Uri(jsseAzureSBURL);

            // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var path = jsseAzureSBURL + apiPath;
            var request = new RestRequest(apiPath, Method.POST);
            var json = JsonConvert.SerializeObject(requestContent);
            request.RequestFormat = DataFormat.Json;
            request.Parameters.Clear();
            //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddObject(requestContent);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            return response;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiPath"></param>
        /// <returns></returns>
        public static Task<T> GetAsync<T>(string apiPath) where T : new ()
        {
            var client = CreateRestClient();
            client.BaseUrl = new Uri(jsseAzureSBURL);
            var taskCompletionSource = new TaskCompletionSource<T>();
            RestRequest request = new RestRequest(apiPath, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.Parameters.Clear();
            //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            try
            {
                client.ExecuteAsync<T>(request, (response) => {
                    var resultDocs = JsonConvert.DeserializeObject<T>(response.Content);
                    taskCompletionSource.SetResult(resultDocs);
                });
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
            return taskCompletionSource.Task;
        }


        /// <summary>
        /// Post Async 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestContent"></param>
        /// <param name="apiPath"></param>
        /// <returns></returns>
        public static Task<T> PostAsync<T>(object requestContent, string apiPath) where T : new()
        {
            var client = CreateRestClient();
            client.BaseUrl = new Uri(jsseAzureSBURL);
            var taskCompletionSource = new TaskCompletionSource<T>();
            RestRequest request = new RestRequest(apiPath, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.Parameters.Clear();
            //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddObject(requestContent);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            try
            {
                client.ExecuteAsync<T>(request, (response) =>
                taskCompletionSource.SetResult(response.Data)
                );
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
            return taskCompletionSource.Task;
        }

        //public static IRestResponse PostRESTAsync(string apiPath, object requestContent)
        //{
        //    RestClient client = CreateRestClient();

        //    client.BaseUrl = new Uri(jsseAzureSBURL);

        //    // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var path = jsseAzureSBURL + apiPath;
        //    var request = new RestRequest(apiPath, Method.POST);
        //    var json = JsonConvert.SerializeObject(requestContent);
        //    request.RequestFormat = DataFormat.Json;
        //    request.Parameters.Clear();
        //    //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
        //    request.AddObject(requestContent);
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("Content-Type", "application/json");
        //    IRestResponse response = client.Execute(request);

        //    return response;

        //}
        private static HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
   
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            string token = GetSASToken(jsseAzureSASKeyName, jsseAzureSASKeyValue);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            return client;
        }

        private static RestClient CreateRestClient()
        {
            RestClient client = new RestClient();

            client.AddDefaultHeader("Content-Type", "application/json");

            string token = GetSASToken(jsseAzureSASKeyName, jsseAzureSASKeyValue);
            client.AddDefaultHeader("Authorization", token);

            return client;
        }
        private static string GetSASToken(string SASKeyName, string SASKeyValue)
        {
            TimeSpan fromEpochStart = DateTime.UtcNow - new DateTime(1970, 1, 1);
            string expiry = Convert.ToString((int)fromEpochStart.TotalSeconds + 3600);
            string stringToSign = WebUtility.UrlEncode(BaseServiceBusAddress) + "\n" + expiry;

            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SASKeyValue));
            string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));

            string sasToken = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
                  WebUtility.UrlEncode(BaseServiceBusAddress), WebUtility.UrlEncode(signature), expiry, SASKeyName);
            return sasToken;
        }
        /// <summary>
        ///     Gets the expiry for a shared access signature token
        /// </summary>
        /// <returns>
        ///     The <see cref="string" /> expiry.
        /// </returns>
        private static string GetExpiry()
        {
            var sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToString((int)sinceEpoch.TotalSeconds + 3600);
        }

        public static string DownloadFile(string apiPath, DocumentDownloadModel.DocumentRequest requestContent)
        {
            RestClient client = CreateRestClient();

            client.BaseUrl = new Uri(jsseAzureSBURL);
            
            var path = jsseAzureSBURL + apiPath;
            var request = new RestRequest(apiPath, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.Parameters.Clear();
            //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddObject(requestContent);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/octet-stream");
            string filePath = string.Empty;
			string statusCode = "";

			try
            {
                filePath = FileSystem.Current.LocalStorage.Path + "/" + requestContent.Name;
				statusCode = "OKOK";
				client.DownloadData(request).SaveAs(filePath);
                filePath = string.Empty;                
            }
            catch (Exception ex)
            {
                // taskCompletionSource.SetException(ex);
            }

           //// byte[] resultFile = null;

            ////resultFile = client.DownloadData(request);


            return statusCode;
        }

    }
}
