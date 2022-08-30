using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Models
{
    public class Responce
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserID { get; set; }
    }
}
