using Firebase.Database;
using Firebase.Database.Query;
using StoreApp.Helpers;
using StoreApp.Models;
using StoreApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services.Implementation
{
    public class OrderService : IOrderPlace
    {
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });

        public async Task<bool> AddOrUpdateOrder(Orders order)
        {
            if (!string.IsNullOrWhiteSpace(order.Key))
            {
                try
                {
                    await firebase.Child(nameof(Orders)).Child(order.Key).PutAsync(order);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                var response = await firebase.Child(nameof(Orders)).PostAsync(order);

                if (response.Key != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public async Task<bool> DeleteOrder(string key)
        {
            try
            {
                await firebase.Child(nameof(Orders)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Orders>> GetAllOrders()
        {
            return (await firebase.Child(nameof(Orders)).OnceAsync<Orders>()).Select(f => new Orders
            {
                
                Debit_CreditCardMethod = f.Object.Debit_CreditCardMethod,
                CashOnDeliveryMethod = f.Object.CashOnDeliveryMethod,
                TotalItems = f.Object.TotalItems,
                SubTotal = f.Object.SubTotal,
                Total = f.Object.Total,
                OrdersList = f.Object.OrdersList,

                Key = f.Key
            }).ToList();

        }
    }
}
