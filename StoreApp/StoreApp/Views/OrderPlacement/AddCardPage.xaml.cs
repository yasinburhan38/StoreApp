using StoreApp.Models;
using StoreApp.ViewModels.AddProductViewModels;
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
    public partial class AddCardPage : ContentPage
    {
        public AddCardPage(Orders order)
        {
            InitializeComponent();

            BindingContext = new AddCardViewModel(order);
        }
    }
}