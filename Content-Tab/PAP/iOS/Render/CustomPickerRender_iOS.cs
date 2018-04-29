using System;
using ConEd.PAP;
using ConEd.PAP.iOS;
using Xamarin.Forms;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using ConEd.PAP.Render;
using ConEd.PAP.iOS.Render;

[assembly: ExportRenderer(typeof(CustomPickerRender), typeof(CustomPickerRender_iOS))]
namespace ConEd.PAP.iOS.Render
{
    public class CustomPickerRender_iOS:PickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.BackgroundColor = UIColor.White;
                Control.BorderStyle = UITextBorderStyle.None;
				Control.Font = UIFont.FromName("Arial", 12);
				Control.TextColor = UIColor.FromRGB(16, 43, 69);
				//Control.Text = "Select Region";
			}
		}
    }
}
