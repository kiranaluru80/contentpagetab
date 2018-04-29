using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace ConEd.PAP.Models
{
    public class Policies
    {
        [PrimaryKey, AutoIncrement]
        public int DocId { get; set; }
        public string DocType { get; set; }
        public string DocSubType { get; set; }
        public string DocName { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsFavourite { get; set; }
        public int DocSubTypeID { get; set; }
        public string ImageSource { get; set; }
    }

    public class PolicySync
    {
        public int ID { get; set; }
        public string LastModified { get; set; }
    }

    public class PoliciesSync
    {
        
        public int ID { get; set; }
        public string LastModified { get; set; }
    }
}
