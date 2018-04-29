using ConEd.PAP.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConEd.PAP.ViewModels
{
    public class FavoritesViewModel : BindableBase
    {

        private ObservableCollection<Policies> items;
        public ObservableCollection<Policies> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }
        public FavoritesViewModel()
        {
            Items = new ObservableCollection<Policies>();
            List<Policies> lstPS=App.PoliciesRepo.GetFavorites();
            foreach (var item in lstPS)
            {
                item.ModifiedDate=item.ModifiedDate.Substring(0, 10);
                Items.Add(item);
            }
        }
    }
}
