using SQLite;
using SQLiteNetExtensions.Extensions;
using StoreApp.Models;
using StoreApp.Utlities;
using StoreApp.Views;
using StoreApp.Views.OrderPlacement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class CartPageViewModel : BaseViewModel
    {
        private List<Products> _List;

        public List<Products> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }

        private Products _demo;

        public Products demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged(); }
        }

        private string _BtnText;

        public string BtnText
        {
            get { return _BtnText; }
            set { _BtnText = value; OnPropertyChanged(); }
        }
        public string UserId { get; set; }

        public ICommand incCmd { get; set; }
        public ICommand decCmd { get; set; }
        public ICommand DualCMD { get; set; }
        public ICommand deleteCMD { get; set; }
        public ICommand BackCMD { get; set; }

        public SQLiteConnection db;
        public CartPageViewModel()
        {
            List = new List<Products>();

            demo = new Products();

            db = SqlUtils.CreateConnection();

            GetItems();

            incCmd = new Command(Increase);
            decCmd = new Command(Decrease);

            DualCMD = new Command(Conditional);

            deleteCMD = new Command(Delete);
            BackCMD = new Command(Back);
        }

        public async Task GetItems()
        {
            try
            {
                List = ReadOperations.GetAllWithChildren<Products>(db);

                demo.SubTotal = List.Sum(s => s.UpdatedPrice);

                if (List.Count == 0 || List == null)
                {
                    BtnText = "Doorgaan met winkelen";
                }
                else
                {
                    BtnText = "Verder naar bestelling " + (List.Count + " items");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        public void Increase(object obj)
        {
            var item = obj as Products;

            if (item.Count != 10)
            {

                item.Count = item.Count + 1;

                item.UpdatedPrice = item.UpdatedPrice + item.Price;

                

                db.Update(item);

                

                GetItems();

            }
        }
        public void Decrease(object obj)
        {
            var item = obj as Products;

            if (item.Count != 1)
            {
                item.Count = item.Count - 1;

                item.UpdatedPrice = item.UpdatedPrice - item.Price;

                

                db.Update(item);

                
            }

            GetItems();
        }
        public void Delete(object obj)
        {
            var item = obj as Products;

            db.Delete(item);

            

            GetItems();
        }
        public async void Conditional(object obj)
        {
            if (BtnText == "Doorgaan met winkelen")
            {
               Application.Current.MainPage = new NavigationPage(new HomePage(/*UserId*/));
            }
            else
            {
               await Application.Current.MainPage.Navigation.PushAsync(new PlaceOrderPage());
            }
        }
        public async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

