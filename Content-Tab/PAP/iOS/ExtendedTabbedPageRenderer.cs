using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using ConEd.PAP.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace ConEd.PAP.iOS
{
    public class ExtendedTabbedPageRenderer:TabbedRenderer
    {
		//protected override void OnElementChanged(VisualElementChangedEventArgs e)
		//{
		//	base.OnElementChanged(e);

		//	// Set Text Font for unselected tab states
		//	UITextAttributes normalTextAttributes = new UITextAttributes();
		//	//normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Light", 20.0F); // unselected

		//	normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Bold", 12.0F);
		//	//normalTextAttributes.TextColor = UIColor.Black;
		//	//UITabBar.Appearance.BackgroundColor = UIColor.
		//	//this.BarBackgroundColor = Color.FromHex("#43AEE7");
		//	//this.BarTextColor = Color.White;
		//	normalTextAttributes.TextColor = UIColor.White;
		//	UITabBarItem.Appearance.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);

		//}

		//public override UIViewController SelectedViewController
		//{
		//	get
		//	{
		//		UITextAttributes selectedTextAttributes = new UITextAttributes();
		//		selectedTextAttributes.Font = UIFont.FromName("ChalkboardSE-Bold", 18.0F); // SELECTED
		//		selectedTextAttributes.TextColor = UIColor.Green;

		//		if (base.SelectedViewController != null)
		//		{
		//			base.SelectedViewController.TabBarItem.SetTitleTextAttributes(selectedTextAttributes, UIControlState.Normal);
		//			//base.SelectedViewController.TabBarItem.ImageInsets=new UIEdgeInsets(10, 0, 10, 0);
		//			//base.SelectedViewController.TabBarItem.TitlePositionAdjustment= UITabBarItem.i
		//		}
		//		return base.SelectedViewController;
		//	}
		//	set
		//	{
		//		base.SelectedViewController = value;

		//		foreach (UIViewController viewController in base.ViewControllers)
		//		{
		//			UITextAttributes normalTextAttributes = new UITextAttributes();
		//			normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Light", 12.0F); // unselected
		//			normalTextAttributes.TextColor = UIColor.White;
		//			viewController.TabBarItem.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
		//			//base.SelectedViewController.TabBarItem.Image = null;
		//		}
		//	}
		//}
    }
}
