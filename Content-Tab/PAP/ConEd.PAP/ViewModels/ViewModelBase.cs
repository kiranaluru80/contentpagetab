using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConEd.PAP.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INotifyPropertyChanged, INavigatedAware
    {
        

        string _docType;
        public string DocType
        {
            get { return _docType; }
            set
            {
                if (_docType != value)
                {
                    _docType = value;
                    //OnPropertyChanged("UserName");
                }
            }
        }
        //public ViewModelBase()
        //{
        //    //this.DocType = DocType;
        //}
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }
        public event PropertyChangedEventHandler PropertChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertChanged;
            if (handler != null)

            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
