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
using System.Diagnostics;
using ConEd.PAP.ExceptionalLogging;

namespace ConEd.PAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DDE : TabbedPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
		public bool callIndicatorservice()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				////StackLoadingRef.IsVisible = false;
			});
			return false;
		}


		private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        public bool isFavorite;
        public bool _favstate;
		public DDE()
        {
            InitializeComponent();
		   
			var navigationPage = Application.Current.MainPage as NavigationPage;
			navigationPage.BarBackgroundColor = Color.FromHex("#43AEE7");
			NavigationPage.SetBackButtonTitle(this, "Back");
            BarBackgroundColor = Color.White;

			var item = new ToolbarItem
            {
                Icon = "topsearch.png"
            };

			//.ToolbarItems.Add(item);

			ToolbarItems.Add(item);

			item.Clicked += (object sender, EventArgs e) =>
			{
                //StacksearchRef.IsVisible = true;
                Navigation.PushAsync(new SearchByCategoryPage());
			};
			PolicyDataModel pdm = new PolicyDataModel();
            ItemsSource = pdm.All;

            if (App.Current.Properties.ContainsKey("cName"))
            {
                this.Title = App.Current.Properties["cName"].ToString();
            }
			//this.BarBackgroundColor = Color.FromHex("#43AEE7");
			//this.BarTextColor = Color.White;


			//NavigationPage.BarBackgroundColorProperty = Color.FromHex("#111111");



			//SearchFactory sf = new SearchFactory();
			//List<Policies> pl=sf.GetSearchResults("","oil");
			

        }


        private void LstDDT_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
				DocumentItem di = new DocumentItem();
				di = (DocumentItem)e.Item;

				string docName = di.DocumentItemTitle;
				 isFavorite = di.IsFavorite;
				di = null;

				ViewDocument dd = new ViewDocument(docName, isFavorite);
				Label lblTitle = dd.FindByName<Label>("lblVDTitle");
				lblTitle.Text = "Policies And Procedures";

				Navigation.PushAsync(dd);
				dd = null;  
            }
            catch(Exception ex)
            {
				logger.Error(ex.Message);
			}

        }

        public string selectedDocName = string.Empty;

        async void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
        {
            try{
				selectedDocName = e.Group.ToString();
				var imageSender = (Image)sender;
                Image _imgstate = (Image)sender;
				var stk = (StackLayout)imageSender.Parent;
				var img = (Image)stk.Children[0];
               // if(imageSender.FindByName<ImageSource>("fav"))
				//overlay
				ContentView ai = this.FindByName<ContentView>("overlay");
                // var answer = await DisplayAlert("", "Would like to mark as favourite", "Yes", "No");
                //if (_imgstate)

				var answer = await DisplayActionSheet("Are you sure to save this pdf to your favorite list? ", "Yes", "No");
				if (answer == "Yes")
				{
					//imageSender.Source = "fav_selected.png";  
					img.Source = "fav_selected.png";
					//ai.IsVisible = true;
					//Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
					//{
					//    //indicator.IsRunning = false;
					//    ai.IsVisible = false;x
					//    return false;
					//});

					//call the save function
					App.PoliciesRepo.UpdateFavorites(selectedDocName, "1");
					ViewDocumentViewModel vdv = new ViewDocumentViewModel();
					vdv.viewDocument(selectedDocName);
                    _favstate = true;
				}
				else
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
                    _favstate = false;
				}
            }
            catch(Exception ex)
            {
				logger.Error(ex.Message);

			}
           
        }

        private void lstDDT_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {
            //DocumentItem di = new DocumentItem();
            try{
				Policies di = new Policies();
				di = (Policies)e.Item;

				string docName = di.DocName;//di.DocumentItemTitle;
                if (_favstate)
                {
                    isFavorite = true;
                }
                else
                {
					isFavorite = di.IsFavourite;// di.IsFavorite;

				}

				di = null;

				ViewDocument dd = new ViewDocument(docName, isFavorite);
				Label lblTitle = dd.FindByName<Label>("lblVDTitle");
				lblTitle.Text = "Policies And Procedures";

				Navigation.PushAsync(dd);
				dd = null;
            }
            catch(Exception ex)
            {
				logger.Error(ex.Message);

			}
          
        }

        //public DDE(string docType)
        //{
        //    this.docType = docType;
        //    //DocumentTypesViewModel dtm = new DocumentTypesViewModel();
        //    //dtm.DocType = docType;
        //    InitializeComponent();

        //}

        //private void LstDDE_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    //Navigation.PushAsync(new ViewDocument());
        //}

        //private void LstDDE_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    Navigation.PushAsync(new ViewDocument(string.Empty,false));
        //}
    }
}