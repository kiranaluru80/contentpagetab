using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConEd.PAP.Models;
using System;


namespace ConEd.PAP.Views
{
	public partial class TabbedPageDemoPage : TabbedPage
	{
        
		public  TabbedPageDemoPage ()
		{
			InitializeComponent();
            //List<string> tabList = new List<string>();
            //tabList.Add("Administrative");
            //tabList.Add("Environmental");
            //tabList.Add("Safety");

            //TabbedPageDemoPageCS tc = new TabbedPageDemoPageCS();
             ItemsSource = MonkeyDataModel.All;
            //ItemsSource = tc.Items;
        }
	}
}
