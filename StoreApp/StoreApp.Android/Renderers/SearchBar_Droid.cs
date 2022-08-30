using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StoreApp.Droid.Renderers;
using StoreApp.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Borderless_SearchBar), typeof(SearchBar_Droid))]
namespace StoreApp.Droid.Renderers
{
    [Obsolete]
    public class SearchBar_Droid : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var color = global::Xamarin.Forms.Color.LightGray;
                var searchView = Control as SearchView;

                int searchPlateId = searchView.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                Android.Views.View searchPlateView = searchView.FindViewById(searchPlateId);
                searchPlateView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}