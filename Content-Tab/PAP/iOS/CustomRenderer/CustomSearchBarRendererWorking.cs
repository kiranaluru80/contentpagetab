using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ConEd.PAP.Controls;
using ConEd.PAP.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRendererWorking))]
namespace ConEd.PAP.iOS.CustomRenderer
{
    public class CustomSearchBarRendererWorking:SearchBarRenderer
    {
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//Override needed, otherwise the original Xamarin code will force show the Cancel button on the right side of the entry field
			if (e.PropertyName == SearchBar.TextProperty.PropertyName)
			{
				Control.Text = Element.Text;
			}

			if (e.PropertyName != SearchBar.CancelButtonColorProperty.PropertyName && e.PropertyName != SearchBar.TextProperty.PropertyName)
				base.OnElementPropertyChanged(sender, e);
		}
    }
}
