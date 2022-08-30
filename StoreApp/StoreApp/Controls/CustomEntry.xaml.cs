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
    public partial class CustomEntry : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TitleTextPropertyChanged);

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable; 
            control.Title.Text = newValue?.ToString(); // gaat eerst controlleren of het null is en daarna omzetten naar string. 
        }//word automatische gegenereerd

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



        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
            typeof(string),
            typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: PlaceholderPropertyChanged);

        private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.Entry.Placeholder = newValue?.ToString();
        }

        public string Placeholder
        {
            get
            {
                return base.GetValue(PlaceholderProperty)?.ToString();
            }

            set
            {
                base.SetValue(PlaceholderProperty, value); OnPropertyChanged();
            }
        }

        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(
                                                   propertyName: nameof(EntryText),
                                                   returnType: typeof(string),
                                                   declaringType: typeof(CustomEntry),
                                                   defaultValue: null,
                                                   defaultBindingMode: BindingMode.TwoWay);
        public string EntryText
        {
            get { return GetValue(EntryTextProperty)?.ToString(); }
            set { SetValue(EntryTextProperty, value); }
        }

        

        public static BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        public static BindableProperty EntryBehaviorProperty = BindableProperty.Create(
            nameof(EntryBehavior),
            typeof(Behavior),
            typeof(CustomEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: EntryBehaviorPropertyChanged);

        public Behavior EntryBehavior
        {
            get => (Behavior)GetValue(EntryBehaviorProperty);
            set => SetValue(EntryBehaviorProperty, value);
        }
        private static void EntryBehaviorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.Entry.Behaviors.Add((Behavior)(newValue));
        }
        public CustomEntry()
        {
            InitializeComponent();

            this.Entry.SetBinding(BorderlessEntry.TextProperty, new Binding(nameof(EntryText), source: this));
            //om de entryText te kunnen gebruiken gaan we die binden

            this.Entry.SetBinding(BorderlessEntry.KeyboardProperty, new Binding(nameof(Keyboard), source: this));

            this.Entry.SetBinding(BorderlessEntry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));

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