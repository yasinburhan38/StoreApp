using StoreApp.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDatePicker : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(CustomDatePicker),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TitleTextPropertyChanged);

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDatePicker)bindable;
            control.Title.Text = newValue?.ToString();
        }

        public string TitleText
        {
            get
            {
                return base.GetValue(TitleTextProperty)?.ToString();
            }

            set
            {
                base.SetValue(TitleTextProperty, value); OnPropertyChanged();
            }
        }
      
        public static readonly BindableProperty MaxDateProperty = BindableProperty.Create(
                                                   propertyName: nameof(MaxDate),
                                                   returnType: typeof(DateTime),
                                                   declaringType: typeof(CustomDatePicker),
                                                   defaultBindingMode: BindingMode.TwoWay,
                                                   propertyChanged: MaxDatePropertyChanged);
        private static void MaxDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDatePicker)bindable;
            control.date.MaximumDate = Convert.ToDateTime(newValue);
        }
        public DateTime MaxDate
        {
            get => (DateTime)GetValue(MaxDateProperty);
            set => SetValue(MaxDateProperty, value);
        }
        public static readonly BindableProperty MinDateProperty = BindableProperty.Create(
                                                   propertyName: nameof(MinDate),
                                                   returnType: typeof(DateTime),
                                                   declaringType: typeof(CustomDatePicker),
                                                   defaultBindingMode: BindingMode.TwoWay,
                                                   propertyChanged: MinDatePropertyChanged);
        private static void MinDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDatePicker)bindable;
            control.date.MinimumDate = Convert.ToDateTime(newValue);
        }
        public DateTime MinDate
        {
            get => (DateTime)GetValue(MinDateProperty);
            set => SetValue(MinDateProperty, value);
        }
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
                                                   propertyName: nameof(Date),
                                                   returnType: typeof(DateTime),
                                                   declaringType: typeof(CustomDatePicker),
                                                   defaultBindingMode: BindingMode.TwoWay,
                                                   propertyChanged: DatePropertyChanged);
        private static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDatePicker)bindable;
            control.date.Date = Convert.ToDateTime(newValue);
        }
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty FormatProperty = BindableProperty.Create(
            nameof(Format),
            typeof(string),
            typeof(CustomDatePicker),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: FormatPropertyChanged);

        private static void FormatPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomDatePicker)bindable;
            control.date.Format = newValue?.ToString();
        }

        public string Format
        {
            get
            {
                return base.GetValue(FormatProperty)?.ToString();
            }

            set
            {
                base.SetValue(FormatProperty, value); OnPropertyChanged();
            }
        }


        public CustomDatePicker()
        {
            InitializeComponent();

            

            entryFrame.BorderColor = Color.LightGray;
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            entryFrame.BorderColor = Color.FromHex("#ffd315");
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            entryFrame.BorderColor = Color.LightGray;
        }
    }
}