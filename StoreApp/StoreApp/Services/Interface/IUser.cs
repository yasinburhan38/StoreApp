using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services.Interface
{
    public interface IUser
    {
        //Task<bool> AddOrUpdateUser(User user);
        Task<bool> RegisterUser(User user);
        Task<bool> UpdateUser(User user);
        Task<User> LoginUser(string email, string password);
        Task<List<User>> GetInfo(string userId);
        Task<bool> DeleteUser(string key);
        //Task<List<User>> GetAllUser(string userId);
    }
}
