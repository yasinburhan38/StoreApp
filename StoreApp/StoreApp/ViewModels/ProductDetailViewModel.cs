using SQLite;
using SQLiteNetExtensions.Extensions;
using StoreApp.Models;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {

        private Products _demo;

        public Products demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged(); }
        }

        private User _user;

        public User user
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private Responce _resp;

        public Responce resp
        {
            get { return _resp; }
            set { _resp = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Products> _List2;

        public ObservableCollection<Products> List2
        {
            get { return _List2; }
            set { _List2 = value; OnPropertyChanged(); }
        }

        public ICommand CartCMD { get; set; }
        public ICommand ViewCartCmd { get; set; }
        public ICommand SelectCmd { get; set; }
        public ICommand BackCmd { get; set; }

        public SQLiteConnection db;

        private readonly IUser _userService;
        public ProductDetailViewModel(Products item)
        {

            demo = new Products();

            user = new User();

            resp = new Responce();

            demo = item;

            demo.rate = 4.5;

            List2 = new ObservableCollection<Products>();

            List2.Add(new Products { Size = "Small" });
            List2.Add(new Products { Size = "Medium" });
            List2.Add(new Products { Size = "Large" });

            List2.Add(new Products { Size = "XL" });
            List2.Add(new Products { Size = "XXL" });
            List2.Add(new Products { Size = "XXXL" });


            db = SqlUtils.CreateConnection();

            _userService = DependencyService.Resolve<IUser>();

            db.CreateTable<Products>();

            GetMyInfo();

            CartCMD = new Command(AddtoCart);
            ViewCartCmd = new Command(Cart);
            SelectCmd = new Command(Select);
            BackCmd = new Command(Back);
        }

        public async Task GetMyInfo()
        {
            try
            {
                var res = ReadOperations.GetAllWithChildren<Responce>(db);

                var max = res.Count;

                var response = await _userService.GetInfo(res[max - 1].UserID);
                //var response = await _userService.GetAllUser(userID);

                user = response[0];

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public async void Select(object obj)
        {
            try
            {
                var item = obj as Products;

                if (item != null)
                {
                    demo.Size = item.Size;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public async void AddtoCart()
        {
            try
            {
                demo.UpdatedPrice = demo.Price;
                demo.Count = 1;
                db.Insert(demo);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public async void Cart()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
        }
        public async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
