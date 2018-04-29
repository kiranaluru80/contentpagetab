using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConEd.PAP.Models;
using ConEd.PAP.ViewModels;
using PCLStorage;
using Xamarin.Forms;

namespace ConEd.PAP.Views
{
    public partial class SearchItemsListPage : ContentPage
    {
        List<Policies> docs = new List<Policies>();
        List<Policies> docsSorted = new List<Policies>();
        string searchitem;
        bool value1 = false;
        public SearchItemsListPage(List<Policies> docsofflinedata, string _item)
        {
            InitializeComponent();
			var navigationPage = Application.Current.MainPage as NavigationPage;
			navigationPage.BarBackgroundColor = Color.FromHex("#43AEE7");
			//docIdRef.Clicked += (sender, e) =>
			//{
			//	lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.DocId).ToList();
			//	sortOrderRef.IsVisible = false;
			//};
			//docTypeRef.Clicked += (sender, e) =>
			//{
			//	lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.DocType).ToList();
			//	sortOrderRef.IsVisible = false;
			//};

			//docSubTypeRef.Clicked += (sender, e) =>
			//{
			//	lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.DocSubType).ToList();
			//	sortOrderRef.IsVisible = false;
			//};

			docNameAscenRef.Clicked += (sender, e) =>
			{
                lstDDT.ItemsSource = docsofflinedata.OrderBy(x => x.DocName).ToList();
				sortOrderRef.IsVisible = false;
                headref.IsVisible = true;
                headref.Text = " Doc Name Ascending Order";
                docNameAscenRef.Image = "DocNameAscendingActive.png";
                modifiedDateAscenRef.Image = "LastModifiedAscendingInactive.png";
                modifiedByAscenRef.Image = "SMEAscendingInactive.png";
                docNameDscenRef.Image = "DocNameDescendingInactive.png";
                modifiedDateDscenRef.Image = "LastModifiedDescendingInactive.png";
                modifiedByDscenRef.Image = "SMEDescendingInactive.png";
			};
            docNameDscenRef.Clicked += (sender, e) =>
              {
                  lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.DocName).ToList();
				  sortOrderRef.IsVisible = false;
				  headref.IsVisible = true;
				  headref.Text = " DocName Descending Order";
				  docNameAscenRef.Image = "DocNameAscendingInactive.png";
				  modifiedDateAscenRef.Image = "LastModifiedAscendingInactive.png";
				  modifiedByAscenRef.Image = "SMEAscendingInactive.png";
				  docNameDscenRef.Image = "DocNameDescendingActive.png";
				  modifiedDateDscenRef.Image = "LastModifiedDescendingInactive.png";
				  modifiedByDscenRef.Image = "SMEDescendingInactive.png";
              };

			modifiedDateAscenRef.Clicked += (sender, e) =>
			{
                lstDDT.ItemsSource = docsofflinedata.OrderBy(x => x.ModifiedDate).ToList();
				sortOrderRef.IsVisible = false;
				headref.IsVisible = true;
				headref.Text = " Modified Date in Ascending Order";
				docNameAscenRef.Image = "DocNameAscendingInactive.png";
				modifiedDateAscenRef.Image = "LastModifiedAscendingActive.png";
				modifiedByAscenRef.Image = "SMEAscendingInactive.png";
				docNameDscenRef.Image = "DocNameDescendingInactive.png";
				modifiedDateDscenRef.Image = "LastModifiedDescendingInactive.png";
				modifiedByDscenRef.Image = "SMEDescendingInactive.png";
			};

			modifiedDateDscenRef.Clicked += (sender, e) =>
			 {
                lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.ModifiedDate).ToList();
				 sortOrderRef.IsVisible = false;
				 headref.IsVisible = true;
				 headref.Text = "Modified By Name in Descending Order";

				 docNameAscenRef.Image = "DocNameAscendingInactive.png";
				 modifiedDateAscenRef.Image = "LastModifiedAscendingInactive.png";
				 modifiedByAscenRef.Image = "SMEAscendingInactive.png";
				 docNameDscenRef.Image = "DocNameDescendingInactive.png";
				 modifiedDateDscenRef.Image = "LastModifiedDescendingActive.png";
				 modifiedByDscenRef.Image = "SMEDescendingInactive.png";
			 };

			modifiedByAscenRef.Clicked += (sender, e) =>
			{
                lstDDT.ItemsSource = docsofflinedata.OrderBy(x => x.ModifiedBy).ToList();
				sortOrderRef.IsVisible = false;
				headref.IsVisible = true;
				headref.Text = "SME Name in Ascending Order";

				docNameAscenRef.Image = "DocNameAscendingInactive.png";
				modifiedDateAscenRef.Image = "LastModifiedAscendingInactive.png";
				modifiedByAscenRef.Image = "SMEAscendingActive.png";
				docNameDscenRef.Image = "DocNameDescendingInactive.png";
				modifiedDateDscenRef.Image = "LastModifiedDescendingInactive.png";
				modifiedByDscenRef.Image = "SMEDescendingInactive.png";

    
			};
           
           
            modifiedByDscenRef.Clicked += (sender, e) =>
              {
                lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.ModifiedBy).ToList();
				  sortOrderRef.IsVisible = false;
				  headref.IsVisible = true;
				  headref.Text = "SME Name in Descending Order";

				  docNameAscenRef.Image = "DocNameAscendingInactive.png";
				  modifiedDateAscenRef.Image = "LastModifiedAscendingInactive.png";
				  modifiedByAscenRef.Image = "SMEAscendingInactive.png";
				  docNameDscenRef.Image = "DocNameDescendingInactive.png";
				  modifiedDateDscenRef.Image = "LastModifiedDescendingInactive.png";
				  modifiedByDscenRef.Image = "SMEDescendingActive.png";
              };
			//isFavouriteRef.Clicked += (sender, e) =>
			//{
			//	lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.IsFavourite).ToList();
			//	sortOrderRef.IsVisible = false;
			//};

			//docSubTypeIdRef.Clicked += (sender, e) =>
			//{
			//	lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.DocSubTypeID).ToList();
			//	sortOrderRef.IsVisible = false;
			//};

			docs = docsofflinedata;
            searchitem = _item;
            lstDDT.ItemsSource = docs;
            this.Title = "search item for " + _item;
            var _sortitem = new ToolbarItem
            {
                //Icon = "search_gray.png"
                Text = "Sort"
            };

            //.ToolbarItems.Add(item);

            ToolbarItems.Add(_sortitem);
            lstDDT.ItemSelected += (sender, e) =>
            {
                try{
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
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            };
            _sortitem.Clicked += (object sender, EventArgs e) =>
              {
                try{
					  //if (value1 == false)
					  //{
					  // lstDDT.ItemsSource = docsofflinedata.OrderByDescending(x => x.DocName).ToList();
					  // value1 = true;
					  //}
					  //else
					  //{
					  // lstDDT.ItemsSource = docsofflinedata.OrderBy(x => x.DocName).ToList();
					  // value1 = false;
					  //}
					  if (sortOrderRef.IsVisible)
					  {
						  sortOrderRef.IsVisible = false;
					  }
					  else
					  {
						  sortOrderRef.IsVisible = true;
					  }
                }
                catch(Exception ex)
                  {
					  Debug.WriteLine(ex.Message);
				  }
                  
              };
            searchRef.Clicked += (object sender, EventArgs e) =>
              {
				  //Navigation.PushAsync(new DocumentsSearchReturnPage());
				  Navigation.PushAsync(new DocumentTypes("favorite"));
			  };
            homebtnRef.Clicked += (object sender, EventArgs e) =>
              {
                  Navigation.PushAsync(new DocumentTypes(""));
              };
            favoriteBtnRef.Clicked += (object sender, EventArgs e) =>
              {
                try{
					  Favorites fs = new Favorites();
					  Navigation.PushAsync(fs);
					  fs = null; 
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
				  
              };

     //       var _norefgesture = new TapGestureRecognizer();

     //       _norefgesture.Tapped+=async (sender, e) =>
     //         {
				 // imgFavouriteT.Source = "fav-unselected.png";
				 //     //if exists delete file and update in sqlite
				 //     App.PoliciesRepo.UpdateFavorites(selectedDocName, "0");

				 //     var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(selectedDocName);
				 //     if (ExistenceCheckResult.FileExists == check)
				 //     {
				 //         IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(selectedDocName);
				 //         await file.DeleteAsync();
				 //     }
			  //};
            //nobtnRef.GestureRecognizers.Add(_norefgesture);

        }

		public string selectedDocName = string.Empty;

		async void OnTapGestureRecognizerTapped(object sender, ItemTappedEventArgs e)
		{
            try{
				selectedDocName = e.Group.ToString();
				var imageSender = (Image)sender;
				var stk = (StackLayout)imageSender.Parent;
				var img = (Image)stk.Children[0];
				//var item=await
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
            }
            catch(Exception ex)
            {
				Debug.WriteLine(ex.Message);
			}
		   
		    //var answer = await DisplayAlert("", "Would like to mark as favourite", "Yes", "No");
		    //if (answer)
		    //{
		    //    //imageSender.Source = "fav_selected.png";  
		    //    img.Source = "fav_selected.png";
		    //    //ai.IsVisible = true;
		    //    //Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
		    //    //{
		    //    //    //indicator.IsRunning = false;
		    //    //    ai.IsVisible = false;
		    //    //    return false;
		    //    //});

		    //    //call the save function
		    //    App.PoliciesRepo.UpdateFavorites(selectedDocName, "1");
		    //    ViewDocumentViewModel vdv = new ViewDocumentViewModel();
		    //    vdv.viewDocument(selectedDocName);

		    //}
		    //else
		    //{
		    //    img.Source = "fav-unselected.png";
		    //    //if exists delete file and update in sqlite
		    //    App.PoliciesRepo.UpdateFavorites(selectedDocName, "0");

		    //    var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(selectedDocName);
		    //    if (ExistenceCheckResult.FileExists == check)
		    //    {
		    //        IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(selectedDocName);
		    //        await file.DeleteAsync();
		    //    }
		    //}
		}

	}
}
