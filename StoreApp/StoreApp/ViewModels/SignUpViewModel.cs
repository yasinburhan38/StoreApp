using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Firebase.Auth;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using StoreApp.Helpers;
using StoreApp.Models;
using StoreApp.Services.Interface;
using StoreApp.Utlities;
using StoreApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
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
        public Responce resp { get; set; }
        public string UserId { get; set; }
        public ICommand SignupCMD { get; set; }

        public FirebaseWebApi webApi;

        public SQLiteConnection db;

        private readonly IUser _userService;
        public SignUpViewModel(Models.User userData)
        {
            webApi = new FirebaseWebApi();

            user = new Models.User();

            resp = new Responce();

            _userService = DependencyService.Resolve<IUser>();

            user = userData;

            db = SqlUtils.CreateConnection();

            

            SignupCMD = new Command(Signup);
        }

        public async void Signup()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApi.WebAPIKey));

                
                IsBusy = true;

                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password);

                await _userService.RegisterUser(user);

                var response = await _userService.LoginUser(user.Email.Trim(), user.Password.Trim());

                resp.UserID = response.UserId.ToString();

                db.Insert(resp);

                var content = await auth.GetFreshAuthAsync();

                var serializedcontnet = JsonConvert.SerializeObject(content);

                Preferences.Set("AuthRefreshToken", serializedcontnet);

                

                IsBusy = false;


                await Application.Current.MainPage.DisplayAlert("Succes!", "Registratie is succesvol.", "Ok");

                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            catch (Exception ex)
            {
                IsBusy = false;

                await Application.Current.MainPage.DisplayAlert("", "Probeer het opnieuw.", "Ok");
                
            }

        }
    }
}
