using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP.Models
{
    public class PolicyModel
    {      

        public class Document
        {
            public string DocumentName { get; set; }
            public string DocSubType { get; set; }
            public string DocType { get; set; }
            public string DocumentPath { get; set; }
            public string Modified { get; set; }
            public string ModifiedBy { get; set; }
        }

        public class DocSubType
        {
            public string Name { get; set; }
            public Document[] Documents { get; set; }
        }

        public class DocType
        {
            public string Name { get; set; }
            //public List<DocSubType> DocSubTypes { get; set; }
            public DocSubType[] DocSubTypes { get; set; }
        }
    }
}
