using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        //public int Id { get; set; }
        public string Key { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State_Region { get; set; }
        public string Company { get; set; }
        public string Pincode { get; set; }
        public DateTime DateofBirth { get; set; }
        
    }
}
