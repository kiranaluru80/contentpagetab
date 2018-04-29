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

namespace ConEd.PAP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentsSearchReturnPage : ContentPage
    {
		//public string inputstr;
		List<Policies> docsofflinedata = new List<Policies>();
		List<Policies> docsBySearchItem = new List<Policies>();
		string searchitem;
           protected async override void OnAppearing()
           {
            
            base.OnAppearing();
			// let's see if we have a user in our belly already
			string msg = "No error";
			try
			{
				docsofflinedata = App.PoliciesRepo.GetAllPolicies();
				listViewDocsRef.ItemsSource = docsofflinedata;
				StacksearchRef.IsVisible = true;
			
			}
			catch (Exception ex)
			{
				msg = ex.Message.ToString();
			
			}
			finally
			{
				//await DisplayAlert("test", msg, "cancel");
			}
		}
        public DocumentsSearchReturnPage()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			searchBarRef.TextChanged += (object sender, TextChangedEventArgs e) =>
			{
				if (searchBarRef.Text.Length > 0)
				{
					listViewDocsRef.ItemsSource = docsofflinedata.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
					listViewDocsRef.IsVisible = true;
					cancelbtnRef.IsVisible = true;
					searchitem = searchBarRef.Text;
				}
				if (searchBarRef.Text.Length == 0)
				{
					listViewDocsRef.IsVisible = false;
				}

			};

			searchBarRef.SearchButtonPressed += (sender, e) =>
			{
				docsBySearchItem = docsofflinedata.Where(x => x.DocName.Contains(searchBarRef.Text)).ToList();
				Navigation.PushAsync(new SearchItemsListPage(docsBySearchItem, searchitem));

			};


			//         entrySearchRef.TextChanged += (object sender, TextChangedEventArgs e) =>
			//         {
			//                  if (entrySearchRef.Text.Length > 0)
			//                 {
			//                     listViewDocsRef.ItemsSource = docsofflinedata.Where(x => x.DocName.Contains(entrySearchRef.Text)).ToList();
			//                     listViewDocsRef.IsVisible = true;
			//                     // cancelbtnRef.IsVisible = true;
			//                      searchitem = entrySearchRef.Text;
			//                 }
			//  if (entrySearchRef.Text.Length == 0)
			//  {
			//      listViewDocsRef.IsVisible = false;
			//  }
			//};


			//searchbtn.Clicked += (object sender, EventArgs e) =>
			//{
			//    docsBySearchItem=docsofflinedata.Where(x => x.DocName.Contains(entrySearchRef.Text)).ToList();
			//    Navigation.PushAsync(new SearchItemsListPage(docsBySearchItem,searchitem));
			//};


			cancelbtnRef.Clicked += (object sender, EventArgs e) =>
			  {

				  StacksearchRef.IsVisible = false;
				  listViewDocsRef.IsVisible = false;
				  cancelbtnRef.IsVisible = false;
				  if (searchBarRef.Text.Length > 0)
				  {
					  searchBarRef.Text = "";

				  }
			  };

			searchRef.Clicked += (object sender, EventArgs e) =>
			  {
				  //NavigateByDocType("CEHSP");
				  docsofflinedata = App.PoliciesRepo.GetAllPolicies();
				  listViewDocsRef.ItemsSource = docsofflinedata;
				  StacksearchRef.IsVisible = true;
			  };

			btnCEHSP.Clicked += (object sender, EventArgs e) =>
			{
				//NavigateByDocType("CEHSP");
				NavigateByTabDocType("CEHSP");
			};

			btnCEHSI.Clicked += (object sender, EventArgs e) =>
			{
				//NavigateByDocType("GEHSI");  
				NavigateByTabDocType("GEHSI");

			};
			btnManual.Clicked += (object sender, EventArgs e) =>
			{
				//NavigateByDocType("Manual");
				NavigateByTabDocType("Manual");

			};
			btnGuidance.Clicked += (object sender, EventArgs e) =>
			{
				NavigateByTabDocType("EH&S Guidance");

			};
			btnEnvSpec.Clicked += (object sender, EventArgs e) =>
			{
				NavigateByTabDocType("ES");
				//GenericLanding dd = SetGenericPage("EnvSpec");
				//Navigation.PushAsync(dd);
			};
			btnRuleBook.Clicked += (object sender, EventArgs e) =>
			{
				//NavigateByDocType("Rule Book");
				//CommonViewModel cvm = new CommonViewModel();
				//cvm.SetOnlineDocumentsToLocal();
				//Navigation.PushAsync(new DDE());
				NavigateByTabDocType("Rule Book");
			};

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

			var tabclosegesture = new TapGestureRecognizer();
			tabclosegesture.Tapped += (object sender, EventArgs e) =>
			  {
				  StacksearchRef.IsVisible = false;
				  searchRef.Image = "search_gray.png";
				  homeimage.Source = "Home_activate.png";
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
			SetDocTypeToTabApp(docTypeName);

			DDE dd = new DDE();
			//DocumentDetails dd = new DocumentDetails();
			//Label lblTitle = dd.FindByName<Label>("lblDDTitle");
			//lblTitle.Text = docTypeName;

			Navigation.PushAsync(dd);
			dd = null;
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

	}
}
