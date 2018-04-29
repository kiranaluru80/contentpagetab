using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ConEd.PAP.Models;
using System.Collections.ObjectModel;
using ConEd.PAP.WebAPI;
using Xamarin.Forms;
using RestSharp;
using System.Globalization;
using System.Collections;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;
using Prism.Commands;
using Microsoft.Practices.ObjectBuilder2;
using ConEd.PAP.Views;
using ConEd.PAP.Data;

namespace ConEd.PAP.ViewModels
{   
    public class DocumentDetailsViewModel:BindableBase
    {
        public static List<Policies> OfflineList { get; set; }
        public List<DocumentItem> OfflineDataItems = new List<DocumentItem>();
        public ObservableCollection<PolicyModel.DocType> PolicyDocTypes { get; set; }
       // public ObservableCollection<PolicyModel.DocSubType> PolicySubTypes { get; set; }
        public string[] PolicySubTypes { get; set; }
        public PolicyModel.Document[] PolicyDocuments { get; set; }

        //from here
        public static IList<DocumentItem> DataItems { get; private set; }
        public ObservableCollection<Grouping<SelectDocumentTypeViewModel, DocumentItem>> DocumentTypes { get; set; }

        public DelegateCommand<Grouping<SelectDocumentTypeViewModel, DocumentItem>> HeaderSelectedCommand
        {
            get
            {
                return new DelegateCommand<Grouping<SelectDocumentTypeViewModel, DocumentItem>>(g =>
                {
                    if (g == null) return;
                    g.Key.Selected = !g.Key.Selected;
                    if (g.Key.Selected)
                    {                        
                        if (OfflineDataItems != null)
                            OfflineDataItems.Where(i => (i.DocumentType.DocumentTypeId == g.Key.DocumentType.DocumentTypeId))
                                .ForEach(g.Add);
                    }
                    else
                    {                        
                        g.Clear();
                    }
                });
            }
        }

        //till here

        public DelegateCommand<Grouping<SelectDocumentTypeViewModel, DocumentItem>> LoadData(string DocName)
        {
            return HeaderSelectedCommand;
        }

        public DocumentDetailsViewModel()
        {
            string categoryName = string.Empty;
            if (App.Current.Properties.ContainsKey("input1"))
            {
                categoryName = App.Current.Properties["input1"].ToString();              
            }          
            
            GetOfflinePolicies(categoryName);   //categoryName = "EH&S Guidance";//"CEHSP" "Manual"   "GEHSI""Manual""Rule Book""ES"

            if (OfflineList != null)
            {
                SetOfflineDocumentDetails(OfflineList);
            }            
        }

        public void SetOnlineDocumentsToLocal()
        {
            WebMethods webapi = new WebMethods();
            webapi.GetPolicies();
            webapi.policysEvent += ((WebMethods webAPISender, EventArgs e2) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    PolicyModel.DocType[] PolicyDocTypes = SimpleJson.DeserializeObject<PolicyModel.DocType[]>(webAPISender.Polocysresponse);

                    List<Policies> OfflineList = new List<Policies>();

                    foreach (PolicyModel.DocType dst in PolicyDocTypes)
                    {
                        DataItems = new ObservableCollection<DocumentItem>();

                        int i = 0;
                        PolicySubTypes = new string[5];
                        foreach (PolicyModel.DocSubType pd in dst.DocSubTypes)
                        {
                            PolicySubTypes[i] = pd.Name.ToString();
                            DocumentType dt3 = new DocumentType { DocumentTypeId = i, DocumentTypeTitle = pd.Name.ToString() };
                            int j = 0;
                            foreach (PolicyModel.Document orgDoc in pd.Documents)
                            {                                
                                DocumentItem pdi = new DocumentItem();
                                pdi.DocumentItemId = j;
                                pdi.DocumentItemTitle = orgDoc.DocumentName;
                                pdi.DocumentType = dt3;

                                DataItems.Add(pdi);

                                Policies OfflineSingleP = new Policies();
                                OfflineSingleP.DocName = orgDoc.DocumentName;
                                OfflineList.Add(OfflineSingleP);
                                OfflineSingleP = null;                               

                                j = j + 1;
                            }
                            i = i + 1;
                        }                                                
                    }

                    
                    //PoliciesRepository pr = new PoliciesRepository();
                    
                    //pr.AddAllPolicies(OfflineList);


                });
            });
           
        }
        private void GetOnlineDocuments()
        {
            WebMethods webapi = new WebMethods();
            if (DataItems != null)
            {
                SetDocumentDetails();
            }
            else
            {
                webapi.GetPolicies();
            }

            webapi.policysEvent += ((WebMethods webAPISender, EventArgs e2) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    PolicyModel.DocType[] PolicyDocTypes = SimpleJson.DeserializeObject<PolicyModel.DocType[]>(webAPISender.Polocysresponse);

                    List<Policies> OfflineList = new List<Policies>();

                    foreach (PolicyModel.DocType dst in PolicyDocTypes)
                    {
                        DataItems = new ObservableCollection<DocumentItem>();

                        int i = 0;
                        PolicySubTypes = new string[5];
                        foreach (PolicyModel.DocSubType pd in dst.DocSubTypes)
                        {
                            PolicySubTypes[i] = pd.Name.ToString();
                            DocumentType dt3 = new DocumentType { DocumentTypeId = i, DocumentTypeTitle = pd.Name.ToString() };
                            int j = 0;
                            foreach (PolicyModel.Document orgDoc in pd.Documents)
                            {
                                //await App.PersonRepo.AddNewPersonAsync(orgDoc.DocumentName);
                                DocumentItem pdi = new DocumentItem();
                                pdi.DocumentItemId = j;
                                pdi.DocumentItemTitle = orgDoc.DocumentName;
                                pdi.DocumentType = dt3;

                                DataItems.Add(pdi);

                                //from here
                                Policies OfflineSingleP = new Policies();
                                OfflineSingleP.DocName = orgDoc.DocumentName;
                                OfflineList.Add(OfflineSingleP);
                                OfflineSingleP = null;

                                //till here

                                j = j + 1;
                            }
                            i = i + 1;
                        }
                        //DocumentTypes = new ObservableCollection<Grouping<SelectDocumentTypeViewModel, DocumentItem>>();

                        if (DataItems != null)
                        {
                            SetDocumentDetails();
                        }
                    }
                });
            });
        }

        private void SetOfflineDocumentDetails(List<Policies> pplList)
        {
            int k = 0;
            //List<DocumentItem> OfflineDataItems = new List<DocumentItem>();
            foreach (Policies p in pplList)
            {
                DocumentItem pdi = new DocumentItem();
                pdi.DocumentItemId = p.DocId;
                pdi.DocumentItemTitle = p.DocName;
                pdi.ModifiedDate = p.ModifiedDate;
                pdi.IsFavorite = p.IsFavourite;
                DocumentType dt = new DocumentType { DocumentTypeId = p.DocSubTypeID, DocumentTypeTitle = p.DocSubType.ToString() };
                pdi.DocumentType = dt;

                OfflineDataItems.Add(pdi);
                k = k + 1;
                
            }

            
            DocumentTypes = new ObservableCollection<Grouping<SelectDocumentTypeViewModel, DocumentItem>>();
            var selectDocumentTypes =
                                            OfflineDataItems.Select(x => new SelectDocumentTypeViewModel { DocumentType = x.DocumentType, Selected = false })
                                                .GroupBy(sd => new { sd.DocumentType.DocumentTypeId })
                                                .Select(g => g.First())
                                                .ToList();
            selectDocumentTypes.ForEach(sd => DocumentTypes.Add(new Grouping<SelectDocumentTypeViewModel, DocumentItem>(sd, new List<DocumentItem>())));

        }

        private void SetDocumentDetails()
        {
            DocumentTypes = new ObservableCollection<Grouping<SelectDocumentTypeViewModel, DocumentItem>>();
            var selectDocumentTypes =
                                            DataItems.Select(x => new SelectDocumentTypeViewModel { DocumentType = x.DocumentType, Selected = false })
                                                .GroupBy(sd => new { sd.DocumentType.DocumentTypeId })
                                                .Select(g => g.First())
                                                .ToList();
            selectDocumentTypes.ForEach(sd => DocumentTypes.Add(new Grouping<SelectDocumentTypeViewModel, DocumentItem>(sd, new List<DocumentItem>())));
        }



        //private async void GetOfflinePolicies(string categoryName) //private async Task<List<Policies>> GetOfflinePolicies()
        //{
        //    List<Policies> pplList = await App.PoliciesRepo.GetAllPeopleAsync(categoryName);

        //    OfflineList = pplList;
        //    //return pplList;
        //}

        private void GetOfflinePolicies(string categoryName) //private async Task<List<Policies>> GetOfflinePolicies()
        {
            List<Policies> pplList = App.PoliciesRepo.GetAllPeopleAsync(categoryName);

            OfflineList = pplList;
            //return pplList;
        }



    }




}
