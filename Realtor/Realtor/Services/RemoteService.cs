using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Realtor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtor.Services
{
    public class RemoteService

    {
        public FirebaseClient firebase = new FirebaseClient("https://realtor-3fdd4.firebaseio.com/");

        public FirebaseStorage firebaseStorage = new FirebaseStorage("realtor-3fdd4.appspot.com");

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await firebaseStorage
                .Child("Realtor Images")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task<bool> ValidateUsername(string username)
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    Username = item.Object.Username,

                }).ToList();

                foreach (var item in userlist)
                {
                    if (item.Username == username)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

        }
        public async Task<bool> SignUpAsync(SignUp signUp)
        {
            try
            {
                

                await firebase
                .Child("Users")
                .PostAsync(new User() 
                {
                    CompanyName = signUp.CompanyName,
                    Username = signUp.Username,
                    Avatar = "https://firebasestorage.googleapis.com/v0/b/realtor-3fdd4.appspot.com/o/Realtor%20Images%2Fprofile.png?alt=media&token=f962ae2e-36c9-4ba2-8810-f76e410e626c",
                    Email = signUp.Email, 
                    PhoneNumber = signUp.PhoneNumber, 
                    Address = signUp.Address, 
                    Password = signUp.Password, 
                    AlternatePhoneNumber = " "
                });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<List<User>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    CompanyName = item.Object.CompanyName,
                    Username = item.Object.Username,
                    Email = item.Object.Email,
                    PhoneNumber = item.Object.PhoneNumber,
                    Address = item.Object.Address,
                    Password = item.Object.Password,
                    AlternatePhoneNumber = item.Object.AlternatePhoneNumber,
                    Avatar = item.Object.Avatar
                }).ToList();
                return userlist;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public async Task<User> LoginAsync(Login login)
        {
            try
            {
                var user = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<User>();
                return user.Where(a => a.Username == login.Username && a.Password == login.Password).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public async Task<User> GetUserProfile(string username)
        {
            try
            {
                var user = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<User>();
                return user.Where(a => a.Username == username).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var toUpdatePerson = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Username == user.Username).FirstOrDefault();

                await firebase
                  .Child("Users")
                  .Child(toUpdatePerson.Key)
                  .PutAsync(new User()
                  {
                      CompanyName = user.CompanyName,
                      Username = user.Username,
                      Email = user.Email,
                      Avatar = user.Avatar,
                      PhoneNumber = user.PhoneNumber,
                      AlternatePhoneNumber = user.AlternatePhoneNumber,
                      Address = user.Address,
                      Password = user.Password,
                  });
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            
        }

        public async Task<bool> AddProperty(RealtorProperty realtorProperty)
        {
            try
            {
                await firebase
                .Child("Realtor Property")
                .PostAsync(new RealtorProperty() 
                {
                    CompanyName = realtorProperty.CompanyName,
                    AccountName = realtorProperty.AccountName,
                    Avatar = realtorProperty.Avatar,
                    ItemName = realtorProperty.ItemName,
                    ItemPrice = realtorProperty.ItemPrice,
                    Description = realtorProperty.Description,
                    Location = realtorProperty.Location,
                    ItemType = realtorProperty.ItemType,
                    Negotiable = realtorProperty.Negotiable,
                    PhoneNumber = realtorProperty.PhoneNumber,
                    Email = realtorProperty.Email,
                    FirstImage = realtorProperty.FirstImage,
                    SecondImage = realtorProperty.SecondImage,
                    ThirdImage = realtorProperty.ThirdImage,
                    FourthImage = realtorProperty.FourthImage
                });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<List<RealtorProperty>> GetAllProperty()
        {
            try
            {
                var propertylist = (await firebase
                .Child("Realtor Property")
                .OnceAsync<RealtorProperty>()).Select(item =>
                new RealtorProperty
                {
                    CompanyName = item.Object.CompanyName,
                    AccountName = item.Object.AccountName,
                    Avatar = item.Object.Avatar,
                    ItemName = item.Object.ItemName,
                    ItemPrice = item.Object.ItemPrice,
                    Description = item.Object.Description,
                    Location = item.Object.Location,
                    ItemType = item.Object.ItemType,
                    Negotiable = item.Object.Negotiable,
                    PhoneNumber = item.Object.PhoneNumber,
                    Email = item.Object.Email,
                    FirstImage = item.Object.FirstImage,
                    SecondImage = item.Object.SecondImage,
                    ThirdImage = item.Object.ThirdImage,
                    FourthImage = item.Object.FourthImage
                }).ToList();
                return propertylist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public async Task<RealtorProperty> GetAllLand()
        {
            try
            {
                var land = await GetAllProperty();
                await firebase
                .Child("Realtor Property")
                .OnceAsync<RealtorProperty>();
                return land.Where(a => a.ItemType == "land").FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

    }
}
