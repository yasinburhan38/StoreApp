using StoreApp.ViewModels;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Views.SignIn_UpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage(User user)
        {
            InitializeComponent();

            BindingContext = new SignUpViewModel(user);
        }

        //private void next_Clicked(object sender, EventArgs e)
        //{
        //    Application.Current.MainPage = new ShellPage();
        //}

        //private void sighIn_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new SigninPage());
        //}

        //private void next_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new SignUpPage2());
        //}
    }
}