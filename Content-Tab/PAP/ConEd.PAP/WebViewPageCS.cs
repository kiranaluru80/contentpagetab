using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConEd.PAP
{
    public class WebViewPageCS : ContentPage
    {
        public WebViewPageCS()
        {
            Padding = new Thickness(0, 100, 0, 0);
            Content = new StackLayout
            {
                Children = {
                    new CustomWebView {
                        Uri = "CEHSP.pdf",//"BookPreview2-Ch18-Rel0417.pdf",
						HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };
        }
    }
}
