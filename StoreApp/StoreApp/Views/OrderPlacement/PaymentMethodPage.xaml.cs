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
    public partial class PaymentMethodPage : ContentPage
    {
        public PaymentMethodPage()
        {
            InitializeComponent();

            BindingContext = new PaymentMethodViewModel();
        }
    }
}