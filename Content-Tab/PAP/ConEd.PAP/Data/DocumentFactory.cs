using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConEd.PAP.Models;

namespace ConEd.PAP.Data
{
    public static class DocumentFactory
    {
        public static IList<DocumentItem> DataItems { get; private set; }

        private static readonly DocumentType DocumentType1 = new DocumentType { DocumentTypeId = 1, DocumentTypeTitle = "CEHSP-Administrative" };
        private static readonly DocumentType DocumentType2 = new DocumentType { DocumentTypeId = 2, DocumentTypeTitle = "CEHSP-Environmental" };
        private static readonly DocumentType DocumentType3 = new DocumentType { DocumentTypeId = 3, DocumentTypeTitle = "CEHSP-Safety" };


        static DocumentFactory()
        {
            DataItems = new ObservableCollection<DocumentItem>()
            {
                new DocumentItem
                {
                    DocumentItemId = 1,
                    DocumentItemTitle = "Item 1",
                    DocumentType = DocumentType1
                },
                new DocumentItem
                {
                    DocumentItemId = 2,
                    DocumentItemTitle = "Item 2",
                    DocumentType = DocumentType1
                },
                new DocumentItem
                {
                    DocumentItemId = 3,
                    DocumentItemTitle = "Item 3",
                    DocumentType = DocumentType2
                },
                new DocumentItem
                {
                    DocumentItemId = 1,
                    DocumentItemTitle = "Item 4",
                    DocumentType = DocumentType2
                },
                new DocumentItem
                {
                    DocumentItemId = 2,
                    DocumentItemTitle = "Item 6",
                    DocumentType = DocumentType3
                },
                new DocumentItem
                {
                    DocumentItemId = 3,
                    DocumentItemTitle = "Item 5",
                    DocumentType = DocumentType3
                }
            };
        }


       

    }
}
