using System.Collections.Generic;
using System.Collections.ObjectModel;
using ConEd.PAP.Models;
using System.Linq;
using System;
using System.Globalization;

namespace ConEd.PAP.Models
{
    public class TabPolicyModel
    {
        public string Name { set; get; }
       
        private ObservableCollection<Policies> policyItems;
        public ObservableCollection<Policies> PolicyItems
        {
            get { return policyItems; }
            set
            {
                policyItems = value;
            }
        }

        public TabPolicyModel()
        { 
        }
      
    }
	
    public class PolicyDataModel
	{
		public string Name { set; get; }
        public ObservableCollection<Policies> Items { set; get; }
      
        public List<TabPolicyModel> All { set; get; }

       
        public PolicyDataModel ()
		{
            string categoryName = string.Empty;
            if (App.Current.Properties.ContainsKey("cName"))
            {
                categoryName = App.Current.Properties["cName"].ToString();
            }
            //get full list of policies
            
            List<Policies> pList = App.PoliciesRepo.GetAllPeopleAsync(categoryName);

            List<string> subTypeList = new List<string>();
            if (categoryName.ToLower()=="manual" || categoryName == "EH&S Guidance" || categoryName == "ES" || categoryName == "Rule Book")
            {
                subTypeList.Add(categoryName);
            }
            else
            {
                subTypeList = pList.Select(x => x.DocSubType).Distinct().ToList<string>();
            }
             
            foreach (var item in subTypeList)
            {
                TabPolicyModel pdm = new TabPolicyModel();
                if (item.Contains("CEHSP")|| item.Contains("GEHSI"))
                {
                    pdm.Name = item.Substring(8);
                }
     
                else
                {
					//pdm.Name = item;
					if (item == "Manual")
					{
						pdm.Name ="Manuals";

					}
					if (item == "EH&S Guidance")
					{
						pdm.Name = "Guidance Documents";

					}
					if (item == "ES")
					{
						pdm.Name = "Environment Specs";

					}
					if (item == "Rule Book")
					{
						pdm.Name = "Rulebooks";

					}

				}
                List<Policies> pSubList = new List<Policies>();
                if (categoryName.ToLower() == "manual" || categoryName == "EH&S Guidance" || categoryName == "ES" || categoryName == "Rule Book")
                {
                    pSubList = pList;
                }
                else
                {
                    pSubList = pList.Where(c => c.DocSubType.ToUpper() == item.ToUpper()).ToList();
                }
                     
                foreach (var pItem in pSubList)
                {
                    //DateTime dt = DateTime.ParseExact(pItem.ModifiedDate.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    //string ss = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    pItem.ModifiedDate = pItem.ModifiedDate.Substring(0,10);
                    if ( pItem.IsFavourite){
                        pItem.ImageSource = "fav_selected.png";
                    }
                    else
                    {
						pItem.ImageSource ="fav-unselected.png";

					}
                    if(pdm.PolicyItems==null)
                    {
                        pdm.PolicyItems = new ObservableCollection<Policies>();
                        pdm.PolicyItems.Add(pItem);
                    }
                    else
                    {
                        pdm.PolicyItems.Add(pItem);
                    }
                    
                    
                }
                if (All == null)
                {
                    All = new List<TabPolicyModel>();
                    All.Add(pdm);
                }
                else
                {
                    All.Add(pdm);
                }
                pdm = null;          


            }

			if (All != null)
			{
                foreach(var item in All)
                {
                    item.PolicyItems = new ObservableCollection<Policies>(item.PolicyItems.OrderBy(x => x.IsFavourite).Reverse<Policies>());
                }
			}      
        }
	}
}
