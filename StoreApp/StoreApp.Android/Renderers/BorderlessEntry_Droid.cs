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

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntry_Droid))]

namespace StoreApp.Droid.Renderers
{
    public class BorderlessEntry_Droid : EntryRenderer
    {
        public BorderlessEntry_Droid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            
            if (Control != null)
            {
                Control.Background = null;
                
            }

            
            if (e.NewElement != null)
            {

            }
        }
    }
}