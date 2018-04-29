using System.IO;
using System.Net;
using ConEd.PAP;
using ConEd.PAP.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PCLStorage;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace ConEd.PAP.iOS
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                customWebView.BackgroundColor = Color.White;
                //string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", customWebView.Uri));
                //string fileName = Path.Combine(@"C:\sukarna\ConEd\", WebUtility.UrlEncode("CEHSP A12.12 - EHS Systems.pdf"));
                //fileName =@"C:\sukarna\ConEd\CEHSP A12.12 - EHS Systems.pdf";


                string fileName = Path.Combine(FileSystem.Current.LocalStorage.Path, string.Format("{0}", customWebView.Uri));
                //string secondfilename=FileSystem.Current.LocalStorage.Path;


                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}
