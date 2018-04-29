using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ConEd.PAP.Models;
using Prism.Commands;
using Microsoft.Practices.ObjectBuilder2;

namespace ConEd.PAP.ViewModels
{
    public class SelectDocumentTypeViewModel
    {
        public DocumentType DocumentType { get; set; }
        public bool Selected { get; set; }
    }
    public class DDEViewModel:ViewModelBase
    {
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
                        Data.DocumentFactory.DataItems.Where(i => (i.DocumentType.DocumentTypeId == g.Key.DocumentType.DocumentTypeId))
                            .ForEach(g.Add);
                    }
                    else
                    {
                        g.Clear();
                    }
                });
            }
        }

        

        public DDEViewModel()
        {
            if (App.Current.Properties.ContainsKey("input1"))
            {
                string output = App.Current.Properties["input1"].ToString();
                //App.Current.Properties.Clear();
            }



            string dt = DocType;
            //for Documents
            DocumentTypes = new ObservableCollection<Grouping<SelectDocumentTypeViewModel, DocumentItem>>();
            var selectDocumentTypes =
                Data.DocumentFactory.DataItems.Select(x => new SelectDocumentTypeViewModel { DocumentType = x.DocumentType, Selected = false })
                    .GroupBy(sd => new { sd.DocumentType.DocumentTypeId })
                    .Select(g => g.First())
                    .ToList();
            selectDocumentTypes.ForEach(sd => DocumentTypes.Add(new Grouping<SelectDocumentTypeViewModel, DocumentItem>(sd, new List<DocumentItem>())));
            //till here
        }
    }

    public class Grouping<TK, T> : ObservableCollection<T>
    {
        public TK Key { get; private set; }

        public Grouping(TK key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                Items.Add(item);
        }
    }
}
