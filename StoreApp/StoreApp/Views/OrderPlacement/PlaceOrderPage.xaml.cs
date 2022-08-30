using StoreApp.Models;
using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views.OrderPlacement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        public PlaceOrderPage()
        {
            InitializeComponent();

            BindingContext = new PlaceOrderViewModel();
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void payment_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentMethodPage());
        }
    }
}