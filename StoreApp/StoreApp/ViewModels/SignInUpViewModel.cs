using Newtonsoft.Json;
using StoreApp.Helpers;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Views.SignIn_UpPages;
using Rg.Plugins.Popup.Extensions;
using StoreApp.Views;
using StoreApp.Behaviors;
using StoreApp.Services.Interface;
using Firebase.Auth;
using SQLite;
using StoreApp.Utlities;

namespace StoreApp.ViewModels
{
    public class SignInUpViewModel : BaseViewModel
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
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }
        private Models.User _user;
        public Models.User user
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private DateTime _maxDate;

        public DateTime maxDate
        {
            get { return _maxDate; }
            set { _maxDate = value; OnPropertyChanged(); }
        }

        private string _ConfirmPassword;

        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; OnPropertyChanged(); }
        }

        public FirebaseWebApi webApi;
        private readonly IUser _userService;
        public SQLiteConnection db;
        public ICommand LoginCMD { get; set; }
        public ICommand SignupCMD { get; set; }

        public Responce resp { get; set; }
        public SignInUpViewModel()
        {
            webApi = new FirebaseWebApi();

            user = new Models.User();

            resp = new Responce();

            LoginCMD = new Command(Login);

            SignupCMD = new Command(Signup);

            maxDate = DateTime.Now;

            _userService = DependencyService.Resolve<IUser>();

            db = SqlUtils.CreateConnection();

            
        }
        public async void Login()
        {

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApi.WebAPIKey));
            try
            {
                IsBusy = true;

                var response = await _userService.LoginUser(user.Email.Trim(), user.Password.Trim());
 
                resp.UserID = response.UserId.ToString();

                
                //{
                db.Insert(resp);
                //}

                if (response != null)
                {
                    try
                    {
                        var auth = await authProvider.SignInWithEmailAndPasswordAsync(user.Email, user.Password);

                        var content = await auth.GetFreshAuthAsync();

                        var serializedcontnet = JsonConvert.SerializeObject(content);

                        Preferences.Set("AuthRefreshToken", serializedcontnet);

                        IsBusy = false;

                        Application.Current.MainPage = new NavigationPage(new HomePage(/*response.UserId.ToString()*/));
                    }

                    catch (Exception ex)
                    {
                        IsBusy = false;

                        await Application.Current.MainPage.DisplayAlert("Alert", "Ongeldig email of wachtwoord", "OK");
                    }
                }
                else
                {
                    IsBusy = false;

                    await Application.Current.MainPage.DisplayAlert("", "Ongeldig email of wachtwoord", "OK");
                }

            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("", "Ongeldig email of wachtwoord", "OK");

                //await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }
        public async void Signup()
        {
            try
            {
                if (user.Password == ConfirmPassword)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage(user));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Wachtwoorden komen niet overeen", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }
    }
}
