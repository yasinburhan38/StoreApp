using StoreApp.Services.Interface;
using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views.SignIn_UpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchItemPage : ContentPage
    {
        public SearchPageViewModel vm;
        private readonly IProduct _productService;
        public SearchItemPage()
        {
            InitializeComponent();

            _productService = DependencyService.Resolve<IProduct>();

            BindingContext = vm = new SearchPageViewModel();
        }
        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text.Trim() != null || searchBar.Text.Trim() != string.Empty || searchBar.Text.Trim() != "")
            {
                vm.List = await _productService.GetProductbyName(searchBar.Text.Trim());
            }

        }
    }
}