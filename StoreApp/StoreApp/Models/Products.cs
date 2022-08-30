using SQLite;
using StoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class Products : BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double UpdatedPrice { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public double rate { get; set; }
        public bool Liked { get; set; }
        public string Color { get; set; }
        //public string Size { get; set; }
        private string _Size;
        public string Size
        {
            get { return _Size; }
            set { _Size = value; OnPropertyChanged(); }
        }
        public int Count { get; set; }
        public int TotalItems { get; set; }

        private double _SubTotal;
        public double SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; OnPropertyChanged(); }
        }
        private double _Total;
        public double Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged(); }
        }
    }
}
