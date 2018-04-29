using System;
using ConEd.PAP;
using ConEd.PAP.iOS;
using Xamarin.Forms;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using ConEd.PAP.Render;
using ConEd.PAP.iOS.Render;
[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePicker_iOS))]
namespace ConEd.PAP.iOS.Render
{
    public class CustomDatePicker_iOS:DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
                Control.BackgroundColor = UIColor.Clear;
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Font = UIFont.FromName("Arial", 16);
				Control.TextColor = UIColor.FromRGB(189, 189, 189);
				Control.Text = "Select Date";
			}
		}
    }
}
