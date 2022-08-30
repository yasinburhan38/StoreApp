using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using SQLiteNetExtensions.Extensions;
using StoreApp.Models;
using StoreApp.Services;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views;
using StoreApp.Views.SignIn_UpPages;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                if (_IsBusy)
                {
                    Application.Current.MainPage.Navigation.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    Application.Current.MainPage.Navigation.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }
        private User _user { get; set; }
        public User user
        {
            get { return _user; }
            set
            {
                _user = value;

                OnPropertyChanged();
            }
        }
        private Products _products { get; set; }
        public Products products
        {
            get { return _products; }
            set
            {
                _products = value;

                OnPropertyChanged();
            }
        }

        private LayoutState _CurrentState;
        public LayoutState CurrentState
        {
            get { return _CurrentState; }
            set { _CurrentState = value; OnPropertyChanged(); }
        }

        private LayoutState _CurrentState2;
        public LayoutState CurrentState2
        {
            get { return _CurrentState2; }
            set { _CurrentState2 = value; OnPropertyChanged(); }
        }

        private List<Products> _List;

        public List<Products> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<object> _List2;

        public ObservableCollection<object> List2
        {
            get { return _List2; }
            set { _List2 = value; OnPropertyChanged(); }
        }


        public SQLiteConnection db;

        private readonly IProduct _productService;
        private readonly IUser _userService;

        public ICommand SelectCmd { get; set; }
        public ICommand ViewCartCmd { get; set; }
        public ICommand SignOutCmd { get; set; }

        public HomeViewModel()
        {
            products = new Products();

            user = new User();

            db = SqlUtils.CreateConnection();

            List = new List<Products>();

            List2 = new ObservableCollection<object>();

            _productService = DependencyService.Resolve<IProduct>();
            _userService = DependencyService.Resolve<IUser>();

            GetMyInfo();

            GetProducts();

            List2.Add(new Products { Title = "Prime", Image = "primebox.png" });
            List2.Add(new Products { Title = "Gifting", Image = "giftbox.png" });
            List2.Add(new Products { Title = "Deal", Image = "hotdeal.png" });
            List2.Add(new Products { Title = "Pets", Image = "pets.png" });
            List2.Add(new Products { Title = "Clothing", Image = "clothing.png" });
            List2.Add(new Products { Title = "Home & DIY", Image = "homeappliance.png" });
            List2.Add(new Products { Title = "Gaming", Image = "game.png" });
            List2.Add(new Products { Title = "Electronics", Image = "gadgets.png" });

            SelectCmd = new Command(Select);

            ViewCartCmd = new Command(Cart);

            SignOutCmd = new Command(Signout);
        }
        public async void GetMyInfo()
        {
            try
            {

                var res = ReadOperations.GetAllWithChildren<Responce>(db);

                var max = res.Count;

                var response = await _userService.GetInfo(res[max - 1].UserID);

                user = response[0];
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public async void GetProducts()
        {
            try
            {
                CurrentState = LayoutState.Loading;

                await Task.Run(async () =>
                {
                    List = await _productService.GetAllProducts();

                });

                CurrentState = LayoutState.None;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public Command SearchCMD
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new SearchItemPage());
                });
            }
        }

        public async void Cart()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
        }

        public async void Signout()
        {
            Preferences.Remove("AuthRefreshToken");
            Application.Current.MainPage = new NavigationPage(new SigninPage());
        }

        public async void Select(object obj)
        {
            try
            {
                var item = obj as Products;

                if (item != null)
                {
                   await Application.Current.MainPage.Navigation.PushAsync(new ProductDetailsView(item));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

    }
}
