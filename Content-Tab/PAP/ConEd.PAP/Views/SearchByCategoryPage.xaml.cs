using ConEd.PAP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConEd.PAP.Models;
using PCLStorage;
using ConEd.PAP.Data;

namespace ConEd.PAP.Views
{
    public partial class SearchByCategoryPage : ContentPage
    {
		List<Policies> docsofflinedataByCategory = new List<Policies>();
        List<Policies> docsnavdata = new List<Policies>();
        string _searchitem;

		public SearchByCategoryPage()
        {
            InitializeComponent();
            BackgroundColor = Color.White;
			if (App.Current.Properties.ContainsKey("cName"))
			{
				this.Title = App.Current.Properties["cName"].ToString();
                string categoryname=App.Current.Properties["cName"].ToString();
				docsofflinedataByCategory = App.PoliciesRepo.GetAllPeopleAsync(categoryname);
				listViewDocsRef.ItemsSource = docsofflinedataByCategory;
				StacksearchRef.IsVisible = true;
			}
			listViewDocsRef.ItemSelected += (sender, e) =>
			{
				activityIndicatorLayout.IsVisible = true;
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
				activityIndicatorLayout.IsVisible = false;
			};
			searchBarRef.TextChanged += (object sender, TextChangedEventArgs e) =>
			{
				//if (searchBarRef.Text.Length > 0)
				//{
				//	listViewDocsRef.ItemsSource = docsofflinedataByCategory.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
				//	listViewDocsRef.IsVisible = true;
				//                _searchitem = searchBarRef.Text;
				//                searchBarRef.IsVisible = true;
				//	cancelbtnRef.IsVisible = true;
				//	if (searchBarRef.Text.Length == 0)
				//	{
				//		listViewDocsRef.ItemsSource = null;

				//	}
				//}
				if (searchBarRef.Text.Length > 0)
				{

					List<Policies> docsoffline = docsofflinedataByCategory.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
					if (docsoffline.Count < 15)
					{
						listViewDocsRef.HeightRequest = docsoffline.Count * 45;
					}
					else
					{
						listViewDocsRef.HeightRequest = 400;
					}
					listViewDocsRef.ItemsSource = docsoffline;
					listViewDocsRef.IsVisible = true;
					_searchitem = searchBarRef.Text;
					//searchbtnRef.IsVisible = true;
					cancelbtnRef.IsVisible = true;
					if (searchBarRef.Text.Length == 0)
					{
						listViewDocsRef.ItemsSource = null;

					}
				}
			};

            searchBarRef.SearchButtonPressed += (sender, e) =>
              {
                docsnavdata=docsofflinedataByCategory.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
                Navigation.PushAsync(new SearchtemListByCatPage(docsnavdata,_searchitem) );
              };

			cancelbtnRef.Clicked += (object sender, EventArgs e) =>
				  {

					  listViewDocsRef.IsVisible = false;
					  cancelbtnRef.IsVisible = false;
					  if (searchBarRef.Text.Length > 0)
					  {
						  searchBarRef.Text = "";
                     
					  }
				  };

		}

        protected override async void OnAppearing()
        {
            // let's see if we have a user in our belly already
            string msg = "No error";
            try
            {


				//AuthenticationResult ar =
				//    await App.PCA.AcquireTokenAsync(App.Scopes, App.PCA.Users.FirstOrDefault());
				//await DisplayAlert("test", ar.AccessToken, "cancel");
				//RefreshUserData(ar.AccessToken);
				//btnSignInSignOut.Text = "Sign out";

			}
            catch(Exception ex)
            {
                 msg = ex.Message.ToString();
                // doesn't matter, we go in interactive more
                //btnSignInSignOut.Text = "Sign in";
            }
            finally
            {
                //await DisplayAlert("test", msg, "cancel");
            }
        }
		private void LstDDT_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			DocumentItem di = new DocumentItem();
			di = (DocumentItem)e.Item;

			string docName = di.DocumentItemTitle;
			bool isFavorite = di.IsFavorite;
			di = null;

			ViewDocument dd = new ViewDocument(docName, isFavorite);
			Label lblTitle = dd.FindByName<Label>("lblVDTitle");
			lblTitle.Text = "Policies And Procedures";

			Navigation.PushAsync(dd);
			dd = null;
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
			var answer = await DisplayAlert("", "Would like to mark as favourite", "Yes", "No");
			if (answer)
			{
				//imageSender.Source = "fav_selected.png";  
				img.Source = "fav_selected.png";
				//ai.IsVisible = true;
				//Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
				//{
				//    //indicator.IsRunning = false;
				//    ai.IsVisible = false;
				//    return false;
				//});

				//call the save function
				App.PoliciesRepo.UpdateFavorites(selectedDocName, "1");
				ViewDocumentViewModel vdv = new ViewDocumentViewModel();
				vdv.viewDocument(selectedDocName);

			}
			else
			{
				img.Source = "fav_default.png";
				//if exists delete file and update in sqlite
				App.PoliciesRepo.UpdateFavorites(selectedDocName, "0");

				var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(selectedDocName);
				if (ExistenceCheckResult.FileExists == check)
				{
					IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(selectedDocName);
					await file.DeleteAsync();
				}
			}
		}

		private void lstDDT_ItemTapped_1(object sender, ItemTappedEventArgs e)
		{
			//DocumentItem di = new DocumentItem();
			Policies di = new Policies();
			di = (Policies)e.Item;

			string docName = di.DocName;//di.DocumentItemTitle;
			bool isFavorite = di.IsFavourite;// di.IsFavorite;
			di = null;

			ViewDocument dd = new ViewDocument(docName, isFavorite);
			Label lblTitle = dd.FindByName<Label>("lblVDTitle");
			lblTitle.Text = "Policies And Procedures";

			Navigation.PushAsync(dd);
			dd = null;
		}
    }
}
