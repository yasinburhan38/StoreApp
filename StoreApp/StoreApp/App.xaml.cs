using SQLite;
using StoreApp.Models;
using StoreApp.Services.Implementation;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views;
using StoreApp.Views.SignIn_UpPages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp
{
    public partial class App : Application
    {

        public SQLiteConnection db;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIyNDM0QDMxMzkyZTMxMmUzMEtUU3Y3TGRlUU52T0xNL2hwbjdVTWVrREZSTldaNzR1cktoU0dLSk1lWE09");

            InitializeComponent();

            db = SqlUtils.CreateConnection();

            db.CreateTable<Responce>();

            DependencyService.Register<IUser, UserService>();

            DependencyService.Register<IProduct, ProductService>();

            DependencyService.Register<IOrderPlace, OrderService>();

            MainPage = new NavigationPage(new SigninPage());

            if (!string.IsNullOrEmpty(Preferences.Get("AuthRefreshToken", "")))
            {
                //MainPage = new ShellPage();
                //MainPage = new NavigationPage(new AddProductPage());
                MainPage = new NavigationPage(new HomePage());
                //MainPage = new NavigationPage(new ProductDetailsView(demo));
            }
            else
            {
                // MainPage = new NavigationPage(new MainPage());
                MainPage = new NavigationPage(new SigninPage());
            }

            //MainPage = new MenuPage();
            //MainPage = new ShellPage();
            //MainPage = new NavigationPage(new SigninPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
