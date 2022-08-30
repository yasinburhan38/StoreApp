using Firebase.Database;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Extensions;
using StoreApp.Helpers;
using StoreApp.Models;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views.OrderPlacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class PlaceOrderViewModel :BaseViewModel
    {
        private User _user;
        public User user
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private List<Products> _List;

        public List<Products> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private Products _demo;
        public Products Demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged(); }
        }

        public ICommand PaymentCMD { get; set; }

        public SQLiteConnection db;

        private readonly IUser _userService;
        public PlaceOrderViewModel()
        {
            user = new User();

            List = new List<Products>();

            Demo = new Products();

            db = SqlUtils.CreateConnection();

            _userService = DependencyService.Resolve<IUser>();

            //List = list;
            //Demo = demo;

            PaymentCMD = new Command(PaymentMethod);

            GetMyInfo();
        }
        public async Task GetMyInfo()
        {
            try
            {
                List = ReadOperations.GetAllWithChildren<Products>(db);

                Demo.SubTotal = List.Sum(s => s.UpdatedPrice);

                Demo.Total = Demo.SubTotal + 20 + 10;

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
        public async void PaymentMethod()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PaymentMethodPage());
        }
    }
}
