using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP.Models
{
    public class DocumentItem
    {
        public int DocumentItemId { get; internal set; }
        public string DocumentItemTitle { get; internal set; }

        public string ModifiedDate { get; internal set; }

        public DocumentType DocumentType { get; set; }
        //public int ItemId { get; internal set; }
        //public string ItemTitle { get; internal set; }

        public bool IsFavorite { get; internal set; }
    }
}
