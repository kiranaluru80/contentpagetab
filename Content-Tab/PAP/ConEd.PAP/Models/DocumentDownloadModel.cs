using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP.Models
{
    public class DocumentDownloadModel
    {
        public class DocumentRequest
        {
            public string Path { get; set; }
            public string Name { get; set; }
        }

        public class DocumentResponse
        {
            public byte[] DocumentResponseData { get; set; }
        }
       
    }
}
