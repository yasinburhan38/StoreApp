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
    public class UserService : IUser
    {
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });

        
        public async Task<bool> RegisterUser(User user)
        {
            var result = await firebase.Child(nameof(User)).PostAsync(new User()
            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,

                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                DateofBirth = user.DateofBirth,

                Gender = user.Gender,
                Country = user.Country,
                City = user.City,
                State_Region = user.State_Region,
                Company = user.Company,
                Pincode = user.Pincode
            });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> UpdateUser(User user)
        {

            if (!string.IsNullOrWhiteSpace(user.Key))
            {
                try
                {
                    await firebase.Child(nameof(User)).Child(user.Key).PutAsync(user);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            
        }
        public async Task<User> LoginUser(string email, string password)
        {
            var GetPerson = (await firebase.Child(nameof(User)).OnceAsync<User>())
                .Where(a => a.Object.Email == email).Where(b => b.Object.Password == password).FirstOrDefault();

            if (GetPerson != null)
            {

                var content = GetPerson.Object as User;
                return content;

            }
            else
            {
                return null;
            }
        }

        //Get all the info of the clients
        public async Task<List<User>> GetInfo(string userId)
        {
            var GetInfo = (await firebase.Child(nameof(User)).OnceAsync<User>()).Where(a => a.Object.UserId.ToString() == userId).Select(item => new User
            {
                UserId = item.Object.UserId,
                Email = item.Object.Email,
                Password = item.Object.Password,
                FullName = item.Object.FullName,
                PhoneNumber = item.Object.PhoneNumber,

                DateofBirth = item.Object.DateofBirth,
                Gender = item.Object.Gender,
                Country = item.Object.Country,
                City = item.Object.City,
                State_Region = item.Object.State_Region,

                Company = item.Object.Company,
                Pincode = item.Object.Pincode,

                Key = item.Key

            }); 

            if (GetInfo != null)
            {
                return new List<User>(GetInfo);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteUser(string key)
        {
            try
            {
                await firebase.Child(nameof(User)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}
