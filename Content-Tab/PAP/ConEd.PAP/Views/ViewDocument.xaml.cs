using Xamarin.Forms;
using ConEd.PAP.ViewModels;
using PCLStorage;
using System;

namespace ConEd.PAP.Views
{
    public partial class ViewDocument : ContentPage
    {
        private string _selectedDocName;
        public string SelectedDocName
        {
            get { return _selectedDocName; }
            set
            {
                if (_selectedDocName != value)
                {
                    _selectedDocName = value;
                    OnPropertyChanged();
                }
            }
        }
		private bool _isFavorite;
		public bool IsFavorite
		{
			get { return _isFavorite; }
			set
			{
				if (_isFavorite != value)
				{
					_isFavorite = value;
					OnPropertyChanged();
				}
			}
		}

		public bool CallWebService()
		{
			// Do you webservice call here //
			Device.BeginInvokeOnMainThread(() =>
			{
                StackLoadingRef.IsVisible = true;
			});
            return true;
		}
        public ViewDocument(string DocName, bool IsFavorite)
        {
            
            InitializeComponent();
			MessagingCenter.Subscribe<ViewDocument>(this, "Hi", (sender) =>
			{
                DisplayAlert("hi", "ravi", "OK");
				// do something whenever the "Hi" message is sent
			});
		//	StackLoadingRef.IsVisible = true;

			        if (!IsFavorite)
			        {
			Device.BeginInvokeOnMainThread(() =>
			{
                StackLoadingRef.IsVisible = false;
			});
			   Device.StartTimer(new TimeSpan(0, 0, 5), CallWebService); //20 secondss
			}

			TapGestureRecognizer _norefgesture = new TapGestureRecognizer();
			_norefgesture.Tapped += async (object sender1, EventArgs e2) =>
				{
					btnViewFav.Image = "fav_gray.png";
					activityIndicatorLayout.IsVisible = false;
					var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(this.SelectedDocName);
					if (ExistenceCheckResult.FileExists == check)
					{
						App.PoliciesRepo.UpdateFavorites(this.SelectedDocName, "0");
						IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(this.SelectedDocName);
						await file.DeleteAsync();
					}
					IsFavorite = false;

				};
			nobtnRef.GestureRecognizers.Add(_norefgesture);
			_norefgesture.NumberOfTapsRequired = 1;

			TapGestureRecognizer _yesrefgesture = new TapGestureRecognizer();
			_yesrefgesture.Tapped += (object sender1, EventArgs e2) =>
				{
					btnViewFav.Image = "fav_selected.png";
                    activityIndicatorLayout.IsVisible = false;
					App.PoliciesRepo.UpdateFavorites(this.SelectedDocName, "1");
					IsFavorite = true;
					ViewDocumentViewModel vdv = new ViewDocumentViewModel();
					vdv.viewDocument(this.SelectedDocName);
				};
			yesbtnref.GestureRecognizers.Add(_yesrefgesture);
			_yesrefgesture.NumberOfTapsRequired = 1;
            this.Title = "Policies and procedures";
            // NavigationPage.SetHasNavigationBar(this, false);
            // actIndicator.IsVisible = true;

            if (IsFavorite)
            {
                btnViewFav.Image = "fav_selected.png"; 
            }
            else
            {
				btnViewFav.Image = "fav_gray.png";

			}
            ViewDocumentViewModel vdm = new ViewDocumentViewModel();
            vdm.viewDocument(DocName);

            cwv.Uri = DocName;// "BookPreview2-Ch18-Rel0417.pdf";            
            this.SelectedDocName = DocName;
			//actIndicator.IsVisible = false;
			btnBackB.Clicked += (sender, e) =>
			{
				Navigation.PopAsync();
			};

			//cwv.Navigated += (sender, e) =>

			cwv.Navigating += (sender, e) =>
            {
                StackLoadingRef.IsVisible = true;
            };
            cwv.Navigated += (sender, e) =>
            {
                StackLoadingRef.IsVisible = false;
            };

		}
		public void OnNavigating(object sender, WebNavigatingEventArgs e)
		{
			activity_indicator.IsVisible = true;
		}

		public void OnNavigated(object sender, WebNavigatedEventArgs e)
		{
			activity_indicator.IsVisible = false;
		}



        /// <summary>
        /// Back button click event
        /// </summary>
        // <param name="sender"></param>
        // <param name="e"></param>
        //private void btnBack_Clicked(object sender, System.EventArgs e)
        //{            
        //    DocumentDetails dd = new DocumentDetails();
        //    //DDE dd = new DDE();
        //    Label lblTitle = dd.FindByName<Label>("lblDDTitle");
        //    //lblTitle.Text = "CEHSP";
        //    lblTitle.Text = "lblDDTitle";
        //    Navigation.PushAsync(dd);
        //    dd = null;
        //    lblTitle = null;
        
        //}

        /// <summary>
        /// on Screen disappearing closing the pdf document
        /// </summary>
        protected override async void OnDisappearing()
        {            
            bool isFavorite = App.PoliciesRepo.IsFavorite(this.SelectedDocName);
            if (!isFavorite)
            {
                var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(this.SelectedDocName);
                if (ExistenceCheckResult.FileExists == check)
                {
                    IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(this.SelectedDocName);
                    await file.DeleteAsync();

                }
            }
        }
		public string selectedDocName = string.Empty;

		private async void btnViewFav_Clicked(object sender, System.EventArgs e)
		{
			Button btnf = this.FindByName<Button>("btnViewFav");
			activityIndicatorLayout.IsVisible = true;
		}

		//public string selectedDocName = string.Empty;

		//private async void btnViewFav_Clicked(object sender, System.EventArgs e)
		//{
		//	Button btnf = this.FindByName<Button>("btnViewFav");
		//	var answer = await DisplayAlert("", "Would like to mark as favourite", "Yes", "No");
		//	if (answer)
		//	{

		//		btnf.Image = "fav_selected.png";

		//		App.PoliciesRepo.UpdateFavorites(this.SelectedDocName, "1");
		//		IsFavorite = true;
		//		ViewDocumentViewModel vdv = new ViewDocumentViewModel();
		//		vdv.viewDocument(this.SelectedDocName);

		//	}
		//	else
		//	{
		//		btnf.Image = "fav_gray.png";
		//		var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(this.SelectedDocName);
		//		if (ExistenceCheckResult.FileExists == check)
		//		{
		//			App.PoliciesRepo.UpdateFavorites(this.SelectedDocName, "0");
		//			IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(this.SelectedDocName);
		//			await file.DeleteAsync();
		//		}
		//		IsFavorite = false;
		//	}
		//}

	}
}
