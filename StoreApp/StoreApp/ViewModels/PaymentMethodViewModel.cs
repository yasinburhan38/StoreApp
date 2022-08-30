using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Extensions;
using StoreApp.Helpers;
using StoreApp.Models;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views.OrderPlacement;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using StoreApp.Views;

namespace StoreApp.ViewModels
{
    public class PaymentMethodViewModel : BaseViewModel
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

        public FirebaseWebApi webApi;

        private readonly IOrderPlace _orderService;

        private readonly IUser _userService;

        public SQLiteConnection db;
        public ICommand OrderCmd { get; set; }
        public ICommand BackCmd { get; set; }
        public PaymentMethodViewModel()
        {

            order = new Orders();

            user = new User();

            order.OrdersList = new List<Products>();

            order.Debit_CreditCardMethod = true;

            db = SqlUtils.CreateConnection();

            _orderService = DependencyService.Resolve<IOrderPlace>();
            _userService = DependencyService.Resolve<IUser>();

            GetItems();

            //webApi = new FirebaseWebApi();

            OrderCmd = new Command(Order);
            BackCmd = new Command(Back);
        }

        public async Task GetItems()
        {
            try
            {
                order.OrdersList = ReadOperations.GetAllWithChildren<Products>(db);

                order.SubTotal = order.OrdersList.Sum(s => s.UpdatedPrice);

                order.Total = order.SubTotal + 30;

                order.TotalItems = order.OrdersList.Count;

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        public async void Order()
        {
            try
            {
                IsBusy = true;

                if (order.CashOnDeliveryMethod == true)
                {
                    order.CashOnDeliveryMethod = true;
                    order.Debit_CreditCardMethod = false;

                    var res = ReadOperations.GetAllWithChildren<Responce>(db);

                    var max = res.Count;

                    var response = await _userService.GetInfo(res[max - 1].UserID);

                    user = response[0];

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

                if (order.Debit_CreditCardMethod == true)
                {
                    order.CashOnDeliveryMethod = false;
                    order.Debit_CreditCardMethod = true;

                    IsBusy = false;

                    await Application.Current.MainPage.Navigation.PushAsync(new AddCardPage(order));
                }
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
