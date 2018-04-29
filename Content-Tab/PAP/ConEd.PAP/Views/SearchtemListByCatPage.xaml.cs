using System;
using System.Collections.Generic;
using ConEd.PAP.Models;
using ConEd.PAP.ViewModels;
using PCLStorage;
using Xamarin.Forms;

namespace ConEd.PAP.Views
{
    public partial class SearchtemListByCatPage : ContentPage
    {
        List<Policies> docs = new List<Policies>();
        string searchitem;
        public SearchtemListByCatPage(List<Policies> docsofflinedata, string _item)
        {
            InitializeComponent();
            docs = docsofflinedata;
            searchitem = _item;
            var item = new ToolbarItem
            {
                Icon = "topsearch.png"
            };
            ToolbarItems.Add(item);
            lstDDT.ItemsSource = docs;
            this.Title = "search item for " + _item;
            lstDDT.ItemSelected += (sender, e) =>
            {
                Policies di = new Policies();
                di = (Policies)e.SelectedItem;

                string docName = di.DocName;//di.DocumentItemTitle;
                bool isFavorite = di.IsFavourite;// di.IsFavorite;
                di = null;

                ViewDocument dd = new ViewDocument(docName, isFavorite);
                Label lblTitle = dd.FindByName<Label>("lblVDTitle");
                lblTitle.Text = "Policies And Procedures";

                Navigation.PushAsync(dd);
                dd = null;
            };
            item.Clicked += (object sender, EventArgs e) =>
            {
                //StacksearchRef.IsVisible = true;
                Navigation.PushAsync(new SearchByCategoryPage());
            };
        }
		public string selectedDocName = string.Empty;

		async void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
		{
			selectedDocName = e.Group.ToString();
			var imageSender = (Image)sender;
			var stk = (StackLayout)imageSender.Parent;
			var img = (Image)stk.Children[0];

			//overlay
			ContentView ai = this.FindByName<ContentView>("overlay");
			activityIndicatorLayout.IsVisible = true;

			var _norefgesture = new TapGestureRecognizer();
			_norefgesture.Tapped += async (object sender1, EventArgs e2) =>
				{
					img.Source = "fav-unselected.png";
				  //if exists delete file and update in sqlite
				  App.PoliciesRepo.UpdateFavorites(selectedDocName, "0");

					var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(selectedDocName);
					if (ExistenceCheckResult.FileExists == check)
					{
						IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(selectedDocName);
						await file.DeleteAsync();
					}
					activityIndicatorLayout.IsVisible = false;

				};
			nobtnRef.GestureRecognizers.Add(_norefgesture);

			var _yesrefgesture = new TapGestureRecognizer();
			_yesrefgesture.Tapped += (object sender1, EventArgs e2) =>
				{
					img.Source = "fav_selected.png";
					//if exists delete file and update in sqlite
					App.PoliciesRepo.UpdateFavorites(selectedDocName, "1");
					ViewDocumentViewModel vdv = new ViewDocumentViewModel();
					vdv.viewDocument(selectedDocName);
					activityIndicatorLayout.IsVisible = false;
				};
			yesbtnref.GestureRecognizers.Add(_yesrefgesture);
			//var answer = await DisplayAlert("", "Would like to mark as favourite", "Yes", "No");
			//if (answer)
			//{
			//	//imageSender.Source = "fav_selected.png";  
			//	img.Source = "fav_selected.png";
			//	//ai.IsVisible = true;
			//	//Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
			//	//{
			//	//    //indicator.IsRunning = false;
			//	//    ai.IsVisible = false;
			//	//    return false;
			//	//});

			//	//call the save function
			//	App.PoliciesRepo.UpdateFavorites(selectedDocName, "1");
			//	ViewDocumentViewModel vdv = new ViewDocumentViewModel();
			//	vdv.viewDocument(selectedDocName);

			//}
			//else
			//{
			//	img.Source = "fav-unselected.png";
			//	//if exists delete file and update in sqlite
			//	App.PoliciesRepo.UpdateFavorites(selectedDocName, "0");

			//	var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(selectedDocName);
			//	if (ExistenceCheckResult.FileExists == check)
			//	{
			//		IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(selectedDocName);
			//		await file.DeleteAsync();
			//	}
			//}
		}

	}
}
