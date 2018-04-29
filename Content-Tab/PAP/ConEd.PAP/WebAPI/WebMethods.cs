using System;
using System.Diagnostics;
using Newtonsoft.Json;
using RestSharp;
using ConEd.PAP.Common;
using ConEd.PAP.Models;
using ConEd.PAP.Views;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Text;
using RestSharp.Extensions;
using PCLStorage;

namespace ConEd.PAP.WebAPI
{
    public class WebMethods
    {
        public string Polocysresponse { get; set; }
        
        public event PolocysEventHandler policysEvent;        

        public delegate void PolocysEventHandler(WebMethods sender, EventArgs e);

        public void GetPolicies()
        {            
            var client = new RestClient(constantfile.GetPolicyUrl);
            var request = new RestRequest(Method.GET);
            client.ExecuteAsync(request, response =>
            {
                this.Polocysresponse = response.Content.Replace("$", "");

                policysEvent(this, new EventArgs());
            });
            
        }


        //public byte[] downloadDocumentresponse { get; set; }
        public bool downloadDocumentresponse { get; set; }
        public event DownloadDocumentEventhandler downloadDocumentEvent;
        public delegate void DownloadDocumentEventhandler(WebMethods sender, EventArgs e);
        public void GetDocument(DocumentDownloadModel.DocumentRequest docReq)
        {
            RestClient client = new RestClient();            

            client.BaseUrl = new Uri(constantfile.BaseUrl);
            RestRequest request = new RestRequest(constantfile.GetDocumentFileUrl, Method.POST); //"api/policies/DownloadFile"
            request.RequestFormat = DataFormat.Json;
            request.Parameters.Clear();
            
            //DocumentDownloadModel.DocumentRequest doc = new DocumentDownloadModel.DocumentRequest() { Name = "CEHSP A12.05 - EHS Training Program.pdf", Path = "" };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/octet-stream");
            request.AddObject(docReq);
            try
            {
                //var resp = client.Execute(request);
                string path = FileSystem.Current.LocalStorage.Path+"/"+ docReq.Name;
                client.DownloadData(request).SaveAs(path);
                this.downloadDocumentresponse = true;
                //downloadDocumentEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                // taskCompletionSource.SetException(ex);
            }


            //var client = new RestClient(constantfile.GetDocumentUrl);
            //var request = new RestRequest(Method.POST);
            //request.AddJsonBody(docReq);
            //client.ExecuteAsync(request, response =>
            //{
            //    this.downloadDocumentresponse = response.RawBytes;
            //    //this.downloadDocumentresponse = Encoding.Unicode.GetString(response.RawBytes, 0, response.Content.Length);


            //    downloadDocumentEvent(this, new EventArgs());
            //});
        }

    }
}

