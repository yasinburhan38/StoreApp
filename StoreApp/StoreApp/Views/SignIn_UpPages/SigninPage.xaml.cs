using StoreApp;
using StoreApp.ViewModels;
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
    public partial class SigninPage : ContentPage
    {
        public SigninPage()
        {
            InitializeComponent();

            BindingContext = new SignInUpViewModel();
        }

        private void Expander_Tapped(object sender, EventArgs e)
        {
            UpExp.IsExpanded = true;
            InExp.IsExpanded = false;
        }

        private void InpExp_Tapped(object sender, EventArgs e)
        {
            UpExp.IsExpanded = false;
            InExp.IsExpanded = true;
        }

        private void gender_Focused(object sender, FocusEventArgs e)
        {
            entryFrame.BorderColor = Color.FromHex("#ffd315");
        }

        private void gender_Unfocused(object sender, FocusEventArgs e)
        {
            entryFrame.BorderColor = Color.LightGray;
        }
    }
}