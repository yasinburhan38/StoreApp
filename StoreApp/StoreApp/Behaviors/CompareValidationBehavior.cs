using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using StoreApp.Renderers;

namespace StoreApp.Behaviors
{
    public class CompareValidationBehavior : Behavior<BorderlessEntry>
    {

        public static BindableProperty TextProperty = BindableProperty.Create<CompareValidationBehavior, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        protected override void OnAttachedTo(BorderlessEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = e.NewTextValue == Text;

            ((BorderlessEntry)sender).TextColor = IsValid ? Color.Black : Color.Red;
        }

        protected override void OnDetachingFrom(BorderlessEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
