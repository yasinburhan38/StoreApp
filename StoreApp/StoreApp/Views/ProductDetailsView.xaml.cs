using StoreApp.Models;
using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsView : ContentPage
    {

        public ProductDetailViewModel vm;
        public ProductDetailsView(Products item)
        {
            InitializeComponent();

            BindingContext = vm = new ProductDetailViewModel(item);
        }
    }
}