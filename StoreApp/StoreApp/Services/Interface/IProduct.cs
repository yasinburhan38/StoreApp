using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services.Interface
{
    public interface IProduct
    {
        Task<bool> AddOrUpdateProduct(Products product);
        Task<bool> DeleteProduct(string key);
        Task<List<Products>> GetAllProducts();

        Task<List<Products>> GetProductbyName(string productName);
    }
}
