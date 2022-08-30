using Rg.Plugins.Popup.Extensions;
using SQLite;
using SQLiteNetExtensions.Extensions;
using StoreApp.Models;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels.AddProductViewModels
{
    public class AddCardViewModel : BaseViewModel
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
        private Orders _order;
        public Orders order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(); }
        }
        private User _user;
        public User user
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        public DateTime minDate { get; set; }

        private readonly IOrderPlace _orderService;
        private readonly IUser _userService;

        public SQLiteConnection db;
        public ICommand OrderCmd { get; set; }
        public ICommand BackCmd { get; set; }
        public AddCardViewModel(Orders ord)
        {
            order = new Orders();

            user = new User();

            order = ord;

            minDate = DateTime.Now;

            _userService = DependencyService.Resolve<IUser>();
            _orderService = DependencyService.Resolve<IOrderPlace>();

            db = SqlUtils.CreateConnection();

            OrderCmd = new Command(Order);
            BackCmd = new Command(Back);
        }

        public async void Order()
        {
            try
            {
                IsBusy = true;

                var res = ReadOperations.GetAllWithChildren<Responce>(db);

                var max = res.Count;

                var response = await _userService.GetInfo(res[max - 1].UserID);

                user = response[0];

                //await _userService.UpdateUser(user);

                order.Email = user.Email;
                order.Contact = user.PhoneNumber;
                order.FullName = user.FullName;
                order.Address = user.City + ", " + user.State_Region + ", " + user.Country;

                await _orderService.AddOrUpdateOrder(order);

                db.DeleteAll(order.OrdersList);

                IsBusy = false;

                await Application.Current.MainPage.DisplayAlert("Succes!", "Uw bestelling is geplaatst", "Ok");

                Application.Current.MainPage = new NavigationPage(new HomePage());

            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        public async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
