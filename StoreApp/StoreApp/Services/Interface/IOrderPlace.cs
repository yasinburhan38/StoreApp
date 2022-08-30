using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services.Interface
{
    public interface IOrderPlace
    {
        Task<bool> AddOrUpdateOrder(Orders order);
        Task<bool> DeleteOrder(string key);
        Task<List<Orders>> GetAllOrders();
    }
}
