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
    public class ProductService : IProduct
    {
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });

        public async Task<bool> AddOrUpdateProduct(Products product)
        {
            if (!string.IsNullOrWhiteSpace(product.Key))
            {
                try
                {
                    await firebase.Child(nameof(Products)).Child(product.Key).PutAsync(product);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                var response = await firebase.Child(nameof(Products)).PostAsync(product);
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

        public async Task<List<Products>> GetProductbyName(string productName)
        {
            var GetProduct = (await firebase.Child(nameof(Products)).OnceAsync<Products>()).Where(a => a.Object.Title.ToString() == productName).Select(f => new Products
            {
                
                Title = f.Object.Title,
                Image = f.Object.Image,
                Price = f.Object.Price,
                Name = f.Object.Name,

                Detail = f.Object.Detail,
                rate = f.Object.rate,
                Liked = f.Object.Liked,
                Color = f.Object.Color,
                Size = f.Object.Size,
                Count = f.Object.Count,
                SubTotal = f.Object.SubTotal,
                Total = f.Object.Total,

            });

            if (GetProduct != null)
            {
                return new List<Products>(GetProduct);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteProduct(string key)
        {
            try
            {
                await firebase.Child(nameof(Products)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Products>> GetAllProducts()
        {
            return (await firebase.Child(nameof(Products)).OnceAsync<Products>()).Select(f => new Products
            {
                Title = f.Object.Title,
                Image = f.Object.Image,
                Price = f.Object.Price,
                Name = f.Object.Name,

                Detail = f.Object.Detail,
                rate = f.Object.rate,
                Liked = f.Object.Liked,
                Color = f.Object.Color,
                Size = f.Object.Size,
                Count = f.Object.Count,
                SubTotal = f.Object.SubTotal,

                Key = f.Key
            }).ToList();

        }
    }
}
