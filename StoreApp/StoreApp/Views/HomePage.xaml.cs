using StoreApp.Services.Interface;
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
    public partial class HomePage : ContentPage
    {
        public HomeViewModel vm;
        private readonly IProduct _productService;
        public HomePage()
        {
            InitializeComponent();

            BindingContext = vm = new HomeViewModel();

            _productService = DependencyService.Resolve<IProduct>();

            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {
                TheCarousel.Position = (TheCarousel.Position + 1) % vm.List2.Count;
                return true;
            }));
        }

        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text.Trim() != null || searchBar.Text.Trim() != string.Empty || searchBar.Text.Trim() != "")
            {
                vm.List = await _productService.GetProductbyName(searchBar.Text.Trim());
            }

        }

        //private void menu_Clicked(object sender, EventArgs e)
        //{
        //    Shell.Current.FlyoutIsPresented = true;
        //}
    }
}