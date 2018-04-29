using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConEd.PAP.ViewModels;
using Microsoft.Identity.Client;
using ConEd.PAP.Models;
using System.Diagnostics;
using ConEd.PAP.ExceptionalLogging;

namespace ConEd.PAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentTypes : ContentPage
    {
		//public string inputstr;
		List<Policies> docsofflinedata = new List<Policies>();
        List<Policies> docsBySearchItem = new List<Policies>();
		private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
		string searchitem;
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
		//public bool CallWebService()
		//{
		//	// Do you webservice call here //
		//	Device.BeginInvokeOnMainThread(() =>
		//	{
		//		StackLoadingRef.IsVisible = false;
		//	});
		//	return false;
		//}
        public DocumentTypes(string isComingFrom)
        {
            InitializeComponent();
			//Device.BeginInvokeOnMainThread(() =>
			//{
			//	StackLoadingRef.IsVisible = true;
			//});
			//Device.StartTimer(new TimeSpan(0, 0, 10), CallWebService); //20 seconds



			string returnOS = DependencyService.Get<DependencyServices.INetworkDependency>().IsNetworkAvailable();
       
			NavigationPage.SetHasNavigationBar(this, false);

			if (isComingFrom == "favorite")
			{
				homeimage.Source = "Home_gray.png";
				imgFavorite.Source = "fav_gray.png";
				searchRef.Image = "search_activate.png";

				docsofflinedata = App.PoliciesRepo.GetAllPolicies();
				listViewDocsRef.ItemsSource = docsofflinedata;
				StacksearchRef.IsVisible = true;
			}
			else
			{
				StacksearchRef.IsVisible = false;
			}

            searchBarRef.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                try{
					if (searchBarRef.Text.Length > 0)
					{
						listViewDocsRef.ItemsSource = docsofflinedata.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
						listViewDocsRef.IsVisible = true;
						cancelbtnRef.IsVisible = true;
						searchitem = searchBarRef.Text;
					}
					if (searchBarRef.Text.Length == 0)
					{
						listViewDocsRef.ItemsSource = null;
					}
                }
                catch(Exception ex)
                {
					logger.Error(ex.Message);
				}
                    

            };

			searchBarRef.SearchButtonPressed += (sender, e) =>
			{
                try
                {
					docsBySearchItem = docsofflinedata.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
					Navigation.PushAsync(new SearchItemsListPage(docsBySearchItem, searchitem)); 
                }
                catch(Exception ex)
                {
					logger.Error(ex.Message);
				} 
			};
            			
                cancelbtnRef.Clicked += (object sender, EventArgs e) =>
                  {
                    try
                       {
						  StacksearchRef.IsVisible = false;
						  listViewDocsRef.IsVisible = false;
						  cancelbtnRef.IsVisible = false;
						  if (searchBarRef.Text.Length > 0)
						   {
							  searchBarRef.Text = "";
						   }
                    
                        }

                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                }

				  };
           // searchBarRef.Placeholder = "";
			searchRef.Clicked += (object sender, EventArgs e) =>
			  {
                  //NavigateByDocType("CEHSP");
                try{
					  homeimage.Source = "Home_gray.png";
					  imgFavorite.Source = "fav_gray.png";
					  searchRef.Image = "search_activate.png";

					  docsofflinedata = App.PoliciesRepo.GetAllPolicies();
					  listViewDocsRef.ItemsSource = docsofflinedata;
					  StacksearchRef.IsVisible = true;

                }
                catch(Exception ex)
                {
					  logger.Error(ex.Message);
				  }
                  
			  };

            btnCEHSP.Clicked += (object sender, EventArgs e) =>
            {
                //NavigateByDocType("CEHSP");
                try{
                    
					NavigateByTabDocType("CEHSP");
				}
                catch(Exception ex)
                {
					//Debug.WriteLine(ex.Message); 
					logger.Error(ex.Message);

				}
            };

            btnCEHSI.Clicked += (object sender, EventArgs e) =>
            {
				//NavigateByDocType("GEHSI");
                try{
					NavigateByTabDocType("GEHSI");
				}
                catch(Exception ex)
                {
					logger.Error(ex.Message);
				}

            };
            btnManual.Clicked += (object sender, EventArgs e) =>
            {
				//NavigateByDocType("Manual");
                try{
					NavigateByTabDocType("Manual");

				}
                catch(Exception ex)
                {
					logger.Error(ex.Message);

				}

            };
            btnGuidance.Clicked += (object sender, EventArgs e) =>
            {
                try{
					NavigateByTabDocType("EH&S Guidance");

				}
                catch(Exception ex)
                {
					logger.Error(ex.Message);

				}
               
            };
            btnEnvSpec.Clicked += (object sender, EventArgs e) =>
            {
                try{
					NavigateByTabDocType("ES");
				}
                catch(Exception ex)
                {
					logger.Error(ex.Message);

				}

            };
            btnRuleBook.Clicked += (object sender, EventArgs e) =>
            {
                try{

					NavigateByTabDocType("Rule Book");
                }
                catch(Exception ex)
                {
					//Debug.WriteLine(ex.Message); 
					logger.Error(ex.Message);

				}
               
            };

			btnCEHSPLand.Clicked += (object sender, EventArgs e) =>
		   {
				//NavigateByDocType("CEHSP");
				try
			   {

				   NavigateByTabDocType("CEHSP");
			   }
			   catch (Exception ex)
			   {
					//Debug.WriteLine(ex.Message); 
					logger.Error(ex.Message);

			   }
		   };

			btnCEHSILand.Clicked += (object sender, EventArgs e) =>
			{
				//NavigateByDocType("GEHSI");
				try
				{
					NavigateByTabDocType("GEHSI");
				}
				catch (Exception ex)
				{
					logger.Error(ex.Message);
				}

			};
			btnManualLand.Clicked += (object sender, EventArgs e) =>
			{
				//NavigateByDocType("Manual");
				try
				{
					NavigateByTabDocType("Manual");

				}
				catch (Exception ex)
				{
					logger.Error(ex.Message);

				}

			};
			btnGuidanceLand.Clicked += (object sender, EventArgs e) =>
			{
				try
				{
					NavigateByTabDocType("EH&S Guidance");

				}
				catch (Exception ex)
				{
					logger.Error(ex.Message);

				}

			};
			btnEnvSpecLand.Clicked += (object sender, EventArgs e) =>
			{
				try
				{
					NavigateByTabDocType("ES");
				}
				catch (Exception ex)
				{
					logger.Error(ex.Message);

				}

			};
			btnRuleBookLand.Clicked += (object sender, EventArgs e) =>
			{
				try
				{

					NavigateByTabDocType("Rule Book");
				}
				catch (Exception ex)
				{
					//Debug.WriteLine(ex.Message); 
					logger.Error(ex.Message);

				}

			};

			listViewDocsRef.ItemSelected += (sender, e) =>
            {
               // activityIndicatorLayout.IsVisible = true;
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
					//Debug.WriteLine(ex.Message);
					logger.Error(ex.Message);

				}
		
                //activityIndicatorLayout.IsVisible = false;
            };

            var tabclosegesture = new TapGestureRecognizer();
            tabclosegesture.Tapped += (object sender, EventArgs e) =>
              {
                try{
					  StacksearchRef.IsVisible = false;
					  searchRef.Image = "search_gray.png";
					  homeimage.Source = "Home_activate.png";
                }
                catch(Exception ex)
                {
					  //Debug.WriteLine(ex.Message); 
					  logger.Error(ex.Message);

				  }
                  
              };
            StacksearchRef.GestureRecognizers.Add(tabclosegesture);
            tabclosegesture.NumberOfTapsRequired = 1;

		}

        private void NavigateByDocType(string docTypeName)
        {
            SetDocTypeToApp(docTypeName);
            DocumentDetails dd = new DocumentDetails();
            Label lblTitle = dd.FindByName<Label>("lblDDTitle");
            lblTitle.Text = docTypeName;

            Navigation.PushAsync(dd);
            dd = null;
            lblTitle = null;
        }

        private void NavigateByTabDocType(string docTypeName)
        {
            try{
				SetDocTypeToTabApp(docTypeName);

				//DDE dd = new DDE();
				//DocumentDetails dd = new DocumentDetails();
				//Label lblTitle = dd.FindByName<Label>("lblDDTitle");
				//lblTitle.Text = docTypeName;
                DDENew dd = new DDENew();
				Navigation.PushAsync(dd);
				dd = null;
            }
            catch(Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                logger.Error(ex.Message);
                Navigation.PushAsync(new MasterDetailPage());
            }
           
            //lblTitle = null;
        }

        private static void SetDocTypeToApp(string docTypeName)
        {
            if (App.Current.Properties.ContainsKey("input1"))
            {
                App.Current.Properties.Remove("input1");
            }

            App.Current.Properties["input1"] = docTypeName;
            App.Current.SavePropertiesAsync();
        }

        private static void SetDocTypeToTabApp(string docTypeName)
        {
            if (App.Current.Properties.ContainsKey("cName"))
            {
                App.Current.Properties.Remove("cName");
            }

            App.Current.Properties["cName"] = docTypeName;
            App.Current.SavePropertiesAsync();
        }

        private static GenericLanding SetGenericPage(string pageTitle)
        {
            GenericLanding dd = new GenericLanding();
            Label lblTitle = dd.FindByName<Label>("lblTitle");
            lblTitle.Text = pageTitle;
            return dd;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //GenericLanding dd = SetGenericPage("My Favorite list");
            //Navigation.PushAsync(dd);
            //dd = null;
            Favorites fs = new Favorites();
            Navigation.PushAsync(fs);
            fs = null;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }
		private double widthObj = 0;
		private double heightObj = 0;
		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);
			if (this.widthObj != width || this.heightObj != height)
			{
				this.widthObj = width;
				this.heightObj = height;
				if (widthObj > heightObj)
				{
					Debug.WriteLine("LandScape");
					portantScapeVisible.IsVisible = false;
					landscapeVisible.IsVisible = true;
				}
				else
				{
					Debug.WriteLine("portient");
					portantScapeVisible.IsVisible = true;
					landscapeVisible.IsVisible = false;
				}
				//reconfigure layout
			}
		}
        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    GenericLanding dd = SetGenericPage("My Favorite list");
        //    Navigation.PushAsync(dd);
        //    dd = null;

        //}
    }
}