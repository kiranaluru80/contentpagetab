using ConEd.PAP.ViewModels;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConEd.PAP.Models;
using System.Diagnostics;

namespace ConEd.PAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favorites : ContentPage
    {
        public Favorites()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            try{
                
		  listfav.ItemSelected += (sender, e) =>
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

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            searchref.Clicked += (object sender, EventArgs e) =>
              {
                Navigation.PushAsync(new DocumentTypes("favorite"));
              };
			}


        //private void btnBack_Clicked_1(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new DocumentTypes());
        //}

        private async void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
        {
            try{
				string selectedDocName = e.Group.ToString();
				var imageSender = (Image)sender;
				//var stk = (StackLayout)imageSender.Parent;
				//var img = (Image)stk.Children[0];


				var answer = await DisplayAlert("", "Would like to mark as favourite", "Yes", "No");
				if (answer)
				{

					//imageSender.Source = "fav_selected.png";

					////call the save function
					//App.PoliciesRepo.UpdateFavorites(selectedDocName, "1");
					//ViewDocumentViewModel vdv = new ViewDocumentViewModel();
					//vdv.viewDocument(selectedDocName);

				}
				else
				{
					//imageSender.Source = "fav_default.png";
					//if exists delete file and update in sqlite
					App.PoliciesRepo.UpdateFavorites(selectedDocName, "0");

					var check = await FileSystem.Current.LocalStorage.CheckExistsAsync(selectedDocName);
					if (ExistenceCheckResult.FileExists == check)
					{
						IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(selectedDocName);
						await file.DeleteAsync();
					}
				}
				await Navigation.PushAsync(new Favorites());
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DocumentTypes(""));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DocumentTypes(""));
        }






        //private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new DocumentTypes());
        //}

        //private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        //{

        //}
    }
}