using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConEd.PAP.Models;
using ConEd.PAP.ViewModels;
using PCLStorage;

namespace ConEd.PAP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentDetails : ContentPage
    {
        public DocumentDetails()
        {
            InitializeComponent();           

            ActivityIndicator ai = this.FindByName<ActivityIndicator>("indicator");
            ai.IsRunning = true;
            ai.IsVisible = true;
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                indicator.IsRunning = false;
                indicator.IsVisible = false;

                return false;
            });

            NavigationPage.SetHasNavigationBar(this, false);
            lstDD.ItemSelected += LstDD_ItemSelected;
            lstDD.ItemTapped += LstDD_ItemTapped;        
           
        }

        private void LstDD_ItemTapped(object sender, ItemTappedEventArgs e)
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
               
        async void ImgFavourite_Tapped(object sender, ItemTappedEventArgs e)
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
                App.PoliciesRepo.UpdateFavorites(selectedDocName,"1");
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

        public string selectedDocName = string.Empty;

        private void LstDD_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DocumentItem di = new DocumentItem();
            di = (DocumentItem)e.SelectedItem;

            selectedDocName = di.DocumentItemTitle;            
        }

        

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var imageSender = (StackLayout)sender;
            var img = (Image)imageSender.Children[0];

            var imgSource = img.Source as FileImageSource;
            
            if (imgSource.File == "plusIcon.png")
            {
                img.Source = "minus.png";
            }
            else
            {
                img.Source = "plusIcon.png";
            }
            img = null;
            imageSender = null;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DocumentTypes(""));
        }

        private void lstDD_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {     
                         

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }
    }

    
}