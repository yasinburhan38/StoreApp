using StoreApp.Models;
using StoreApp.Services.Interface;
using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private List<Products> _List;

        public List<Products> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private readonly IProduct _productService;
        public ICommand SelectCmd { get; set; }
        public SearchPageViewModel()
        {
            List = new List<Products>();

            _productService = DependencyService.Resolve<IProduct>();

            SelectCmd = new Command(Select);
        }
        public async void Select(object obj)
        {
            try
            {
                var item = obj as Products;

                if (item != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new ProductDetailsView(item));
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public Command BackCmd
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }
    }
}
