using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConEd.PAP.ViewModels
{
    public class DocumentTypesViewModel : ViewModelBase
    {
        string _docType;
        public string DocTypeName
        {
            get { return _docType; }
            set
            {
                if (_docType != value)
                {
                    _docType = value;
                    DocType = _docType;
                }
            }
        }
        public DocumentTypesViewModel()
        {
            //DocType = DocTypeName;
        }
    }
}
