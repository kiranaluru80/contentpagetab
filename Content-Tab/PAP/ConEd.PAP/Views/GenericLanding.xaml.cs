using Xamarin.Forms;

namespace ConEd.PAP.Views
{
    public partial class GenericLanding : ContentPage
    {
        public GenericLanding()
        {
            InitializeComponent();
            //lblTitle.Text=PageTitle;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public string PageTitle { get; internal set; }

        private void btnBack_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DocumentTypes(""));

        }
    }
}
