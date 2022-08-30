using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class Orders
    {
        //public int Id { get; set; }
        public string Key { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool Debit_CreditCardMethod { get; set; }
        public string CardName { get; set; }
        public string CardPin { get; set; }
        public DateTime CardExpiration { get; set; }
        public bool CashOnDeliveryMethod { get; set; }
        public int TotalItems { get; set; }
        public double Total { get; set; }
        public double SubTotal { get; set; }
        public List<Products> OrdersList { get; set; }
    }
}
