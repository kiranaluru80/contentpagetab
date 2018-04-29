using ConEd.PAP.Common;
using ConEd.PAP.Data;
using ConEd.PAP.Models;
using ConEd.PAP.WebAPI;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConEd.PAP.ViewModels
{
    public class CommonViewModel
    {
        public ObservableCollection<DocumentItem> DataItems { get; private set; }
        public string[] PolicySubTypes { get; private set; }

        public string lastSyncSqliteDate = string.Empty;

        public void SetOnlineDocumentsToLocal()
        {
            string policyUrl = constantfile.GetPolicyUrl;
            //call policy last modified from sqlite
           GetSQLiteLastSyncDate();// App.PoliciesRepo.GetSQLiteLastSyncDate();

           // App.PoliciesRepo.UpdateSync();
           
            if (lastSyncSqliteDate!=string.Empty)
            {
                policyUrl = policyUrl + "?lastSyncDate=" + lastSyncSqliteDate;
            }

          
            ServiceBusRelay.GetAsync<List<PolicyModel.DocType>>(policyUrl).ContinueWith((taskResponse) =>
            {
                var docResult = taskResponse;
                docResult.Wait();                
                var docs = new List<PolicyModel.DocType>(docResult.Result);

                bool areModifications = false;
                foreach (var item in docs)
                {                    
                    foreach (PolicyModel.DocSubType pd in item.DocSubTypes)
                    {
                        int ss = pd.Documents.Count();
                        if (ss>0)
                        {
                            areModifications = true;
                            break;
                        }
                    }
                    if (areModifications) break;
                }
            if (areModifications)
            {
                List<Policies> OfflineList = new List<Policies>();                
                IEnumerable<PolicyModel.DocSubType[]> docsublist = docs.Where(c => (c.Name=="GEHSI"||c.Name=="CEHSP")).Select(x => x.DocSubTypes).Distinct();
                //IEnumerable<PolicyModel.DocSubType[]> docsublist = docs.Select(x => x.DocSubTypes).Distinct();
                //int docTypeID = 0;
                List<Dictionary<string, string>> dictList = new List<Dictionary<string, string>>();                
            
                foreach (var item in docsublist)
                {
                    int i = 0;
                    foreach (var docItem in item)
                    {
                        Dictionary<string, string> docTypeDictionary = new Dictionary<string, string>();                            
                        docTypeDictionary.Add(docItem.Name.ToString(), (i + 1).ToString());
                        dictList.Add(docTypeDictionary);
                                                     
                        docTypeDictionary = null;
                        i = i + 1;
                    }            
                }
                
                foreach (var item in docs)
                {                    
                    int i = 0;
                    int DocID = 1;
                    PolicySubTypes = new string[5];
                    foreach (PolicyModel.DocSubType pd in item.DocSubTypes)
                    {
                        PolicySubTypes[i] = pd.Name.ToString();                        
                        int j = 1;
                        foreach (PolicyModel.Document orgDoc in pd.Documents)
                        {
                            Policies OfflineSingleP = new Policies();
                            OfflineSingleP.DocId = DocID;
                            OfflineSingleP.DocType = orgDoc.DocType;
                            OfflineSingleP.DocSubType = orgDoc.DocSubType;
                            OfflineSingleP.DocName = orgDoc.DocumentName;
                            OfflineSingleP.ModifiedDate = orgDoc.Modified;
                            OfflineSingleP.ModifiedBy = orgDoc.ModifiedBy;
                            OfflineSingleP.IsFavourite = false;
                            if(orgDoc.DocType == "CEHSP"|| orgDoc.DocType == "GEHSI")
                            {
                                OfflineSingleP.DocSubTypeID = getDocumentSubTypeID(dictList, orgDoc.DocSubType);//j; 
                            }
                            else
                            {
                                OfflineSingleP.DocSubTypeID = 1;
                            }
                                                                                  

                            OfflineList.Add(OfflineSingleP);
                            OfflineSingleP = null;
                            
                            DocID = DocID + 1;
                        }
                        j = j + 1;
                        i = i + 1;
                    }
                }

                    //delete data from sqlite
                    App.PoliciesRepo.DeleteAllPolicies();

                    //insert data to sqlite
                  //  App.PoliciesRepo.AddAllPolicies(OfflineList);
                   // App.PoliciesRepo.UpdateLastModified(OfflineList);
                }

            });
           
            

        }

        

        private int getDocumentSubTypeID(List<Dictionary<string, string>> dictList, string DocSubType)
        {
            //dictList = dictList.Distinct<string, string>();
            int docSubTypeID = 0;
            foreach (var dict in dictList)
            {
                string docSubID = string.Empty;
                if (dict.Keys.Contains(DocSubType))
                {
                    docSubID = dict.FirstOrDefault(x => x.Key != null && x.Key.Contains(DocSubType)).Value;
                    if (docSubID != null)
                    {
                        docSubTypeID = Convert.ToInt32(docSubID);
                    }
                    break;
                }
                
                //string ss= dict.FirstOrDefault(x => x.Key != null && x.Key.Contains(orgDoc.DocSubType)).Value;
            }
            return docSubTypeID;
        }

        public string GetLastSyncDate()
        {
            GetSQLiteLastSyncDate();
            return lastSyncSqliteDate;
        }
        public string GetSQLiteLastSyncDate()
        {
            lastSyncSqliteDate = App.PoliciesRepo.GetSQLiteLastSyncDate();
            return lastSyncSqliteDate;
        }

//        foreach (var item in docsublist)
//                {
//                        int i = 0;
//                        foreach (var docItem in item)
//                        {
//                            Dictionary<string, string> docTypeDictionary = new Dictionary<string, string>();

        //                            if (dictList.Count != 0)
        //                            {
        //                                bool isDuplicate = false;
        //                                foreach (var dict in dictList)
        //                                {
        //                                    if (dict.Keys.Contains(docItem.Name.ToString()))
        //                                    {
        //                                        isDuplicate = true;
        //                                        break;                                        
        //                                    }
        //}
        //                                if(!isDuplicate)
        //                                {
        //                                    docTypeDictionary.Add(docItem.Name.ToString(), (i + 1).ToString());
        //                                    dictList.Add(docTypeDictionary);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                docTypeDictionary.Add(docItem.Name.ToString(), (i + 1).ToString());
        //                                dictList.Add(docTypeDictionary);
        //                            }                            
        //                            docTypeDictionary = null;
        //                            i = i + 1;
        //                        }            
        //                }

    }
}
