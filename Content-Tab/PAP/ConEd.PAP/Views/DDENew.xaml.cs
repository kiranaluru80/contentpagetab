using System;
using System.Collections.Generic;
using ConEd.PAP.Models;
using ConEd.PAP.TabbedPages;
using Xamarin.Forms;

namespace ConEd.PAP.Views
{
    public partial class DDENew : TabbedPage
    {
        public DDENew()
        {
            InitializeComponent();

            PolicyDataModel pdm = new PolicyDataModel();
            var count = pdm.All;

            switch (pdm.All.Count)
            {
                case 1:
                    this.Children.Add(new FirstPage(pdm.All[0]) { Title = pdm.All[0].Name, Icon = pdm.All[0].Name});
                    break;
                case 2:
                    this.Children.Add(new FirstPage(pdm.All[0]) { Title = pdm.All[0].Name, Icon =pdm.All[0].Name});
                    this.Children.Add(new SecondPage(pdm.All[1]) { Title = pdm.All[1].Name, Icon =pdm.All[1].Name });
                    break;
                case 3:
                    this.Children.Add(new FirstPage(pdm.All[0]) { Title = pdm.All[0].Name, Icon =pdm.All[0].Name});
                    this.Children.Add(new SecondPage(pdm.All[1]) { Title = pdm.All[1].Name, Icon = pdm.All[1].Name });
                    this.Children.Add(new ThirdPage(pdm.All[2]) { Title = pdm.All[2].Name, Icon = pdm.All[2].Name });
                    break;
                case 4:
                    this.Children.Add(new FirstPage(pdm.All[0]) { Title = pdm.All[0].Name, Icon = pdm.All[0].Name });
                    this.Children.Add(new SecondPage(pdm.All[1]) { Title = pdm.All[1].Name, Icon = pdm.All[1].Name });
                    this.Children.Add(new ThirdPage(pdm.All[2]) { Title = pdm.All[2].Name, Icon = pdm.All[2].Name });
                    this.Children.Add(new ThirdPage(pdm.All[3]) { Title = pdm.All[3].Name, Icon = pdm.All[3].Name });
                    break;
                default:
                    break;
            }

        }
    }
}
