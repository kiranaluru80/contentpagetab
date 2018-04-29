using ConEd.PAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP.Data
{
    public class SearchFactory
    {
        public SearchFactory()
        {

        }
        public List<Policies> GetSearchResults(string category, string search)
        {
            search = "oil";
            List<Policies> wholeList = new List<Policies>();
            if (category==string.Empty)
            {
                wholeList = App.PoliciesRepo.GetAllPolicies();
            }
            else
            {
                wholeList = App.PoliciesRepo.GetAllPeopleAsync(category);
            }
            
            wholeList = wholeList.Where(s => s.DocName.Contains(search)).ToList<Policies>();
            return wholeList;
        }
    }
}
