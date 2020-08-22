using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Realtor.Utils
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants   

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        #region CompanyName Constants   
        private const string CompanyName = "companyName";
        private static readonly string CompanyNameDefault = string.Empty;
        #endregion

        public static string CompanyNameSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(CompanyName, CompanyNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CompanyName, value);
            }
        }

        #region Username Constants   
        private const string UsernameKey = "settings_key";
        private static readonly string UsernameDefault = string.Empty;
        #endregion

        public static string UsernameSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(UsernameKey, UsernameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsernameKey, value);
            }
        }

        #region Email Constants   
        private const string Email = "email";
        private static readonly string EmailDefault = string.Empty;
        #endregion

        public static string EmailSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(Email, EmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(Email, value);
            }
        }

        #region PhoneNumber Constants   
        private const string PhoneNumber = "phoneNumber";
        private static readonly string PhoneNumberDefault = string.Empty;
        #endregion

        public static string PhoneNumberSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(PhoneNumber, PhoneNumberDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PhoneNumber, value);
            }
        }

        #region Address Constants   
        private const string Address = "address";
        private static readonly string AddressDefault = string.Empty;
        #endregion

        public static string AddressSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(Address, AddressDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(Address, value);
            }
        }

        #region Avatar Constants   
        private const string Avatar = "Avatar";
        private static readonly string AvatarDefault = string.Empty;
        #endregion

        public static string AvatarSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(Avatar, AvatarDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(Avatar, value);
            }
        }

        #region AlternatePhoneNumber Constants   
        private const string AlternatePhoneNumber = "alternatePhoneNumber";
        private static readonly string AlternatePhoneNumberDefault = string.Empty;
        #endregion

        public static string AlternatePhoneNumberSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(AlternatePhoneNumber, AlternatePhoneNumberDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AlternatePhoneNumber, value);
            }
        }

        
        #region Password Constants   
        private const string Password = "password";
        private static readonly string PasswordDefault = string.Empty;
        #endregion

        public static string PasswordSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(Password, PasswordDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(Password, value);
            }
        }

        


        public static void ClearAllData()
        {
            AppSettings.Clear();
        }

        //#region Setting Constants

        //private const string username = "username";
        //private static readonly string UsernameDefault = string.Empty;

        //#endregion


        //public static string UsernameSettings
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault(username, UsernameDefault);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue(username, value);
        //    }
        //}



    }
}
