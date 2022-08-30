using Foundation;
using StoreApp.iOS.Renderers;
using StoreApp.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Borderless_SearchBar), typeof(SearchBar_IOS))]
namespace StoreApp.iOS.Renderers
{
    public class SearchBar_IOS : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Control != null)
            {
                Control.ShowsCancelButton = false;

                UITextField txSearchField = (UITextField)Control.ValueForKey(new Foundation.NSString("searchField"));
                txSearchField.BackgroundColor = UIColor.White;
                txSearchField.BorderStyle = UITextBorderStyle.None;
                txSearchField.Layer.BorderWidth = 1.0f;
                txSearchField.Layer.CornerRadius = 2.0f;
                txSearchField.Layer.BorderColor = UIColor.LightGray.CGColor;

            }
        }
    }
}