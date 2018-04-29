using System;
using System.Collections.Generic;
using ConEd.PAP.Models;
using Xamarin.Forms;

namespace ConEd.PAP.TabbedPages
{
    public partial class ThirdPage : ContentPage
    {
        public ThirdPage(TabPolicyModel policyModel)
        {
            InitializeComponent();
            lstDDT.ItemsSource = policyModel.PolicyItems;
        }
    }
}
