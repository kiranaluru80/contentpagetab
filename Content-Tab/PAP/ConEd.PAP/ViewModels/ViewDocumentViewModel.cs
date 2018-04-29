using ConEd.PAP.Models;
using ConEd.PAP.WebAPI;
using PCLStorage;
using Prism.Commands;
using Prism.Mvvm;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConEd.PAP.DependencyServices;
using ConEd.PAP.Views;

namespace ConEd.PAP.ViewModels
{
    public class ViewDocumentViewModel : BindableBase
    {
        public bool isFavorite=false;
		public string returnOS = DependencyService.Get<INetworkDependency>().IsNetworkAvailable();

		public string selectedDocName { get; set; }
        public ViewDocumentViewModel()
        {
            //bool isFavorite = false;// await FileSystem.Current.LocalStorage.CheckExistsAsync(fileName);
            //bool x=await Isfavourite("test.pdf");

            //viewDocument(selectedDocName);
            //Isfavourite("test.pdf");
            //if (isFavorite)
            //{
            //    string ss = "";

            //}
            //else
            //{
            //    WebMethods webapi = new WebMethods();
            //    DocumentDownloadModel.DocumentRequest dr = new DocumentDownloadModel.DocumentRequest();
            //    dr.Name = "string";
            //    dr.Path = "GEHSI E07.23 - Cleanup of PCB Spills.pdf";//"";GEHSI E02.01 - Spill Reporting.pdf

            //    webapi.GetDocument(dr);

            //    webapi.downloadDocumentEvent += ((WebMethods webAPISender, EventArgs e2) =>
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            byte[] drd;

            //        //drd = SimpleJson.DeserializeObject<string>(webAPISender.downloadDocumentresponse);
            //        //drd = SimpleJson.DeserializeObject<byte[]>(webAPISender.downloadDocumentresponse);

            //        //MemoryStream ms = new MemoryStream();


            //        drd = webAPISender.downloadDocumentresponse;
            //        //foreach (byte item in drd)
            //        //{
            //        //    ms.WriteByte(item);
            //        //}
            //        MemoryStream ms = new MemoryStream(drd);

            //        //ms.WriteAsync(drd, 0);
            //        SaveFile(ms);
            //        });
            //    });


            //}

            //webapi.GetPolicies();
            //webapi.policysEvent += ((WebMethods webAPISender, EventArgs e2) =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        PolicyModel.DocType[] PolicyDocTypes = SimpleJson.DeserializeObject<PolicyModel.DocType[]>(webAPISender.Polocysresponse);
            //    });
            //});
        }


        public async void viewDocument(string selectedDocName)
        {
            await Isfavourite(selectedDocName);
            if (!isFavorite)//!isFavorite
            {
                DocumentDownloadModel.DocumentRequest dr = new DocumentDownloadModel.DocumentRequest();
                dr.Path = "";
                dr.Name = selectedDocName;// "GEHSI E07.23 - Cleanup of PCB Spills.pdf";//"";GEHSI E02.01 - Spill Reporting.pdf
                if(returnOS=="Available")
                {
					string statusCode = ServiceBusRelay.DownloadFile("api/policies/DownloadFile", dr);

				}
                else
                {
                    
                }
            }
            
        }


        public async void SaveFile(MemoryStream ms)
        {
            await FileManager.SaveFileAsync("", ms);
        }

        public async Task Isfavourite(string fileName)
        {
            //await FileSystem.Current.LocalStorage.CheckExistsAsync(fileName);
            //bool isFavorite = false;
            var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(fileName);
            if (ExistenceCheckResult.FileExists == check)
            {
                isFavorite = true;       



            }
            
        }
    }
}
