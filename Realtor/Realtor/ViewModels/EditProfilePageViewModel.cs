using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using Realtor.Services;
using Realtor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Realtor.ViewModels
{
    public class EditProfilePageViewModel : ViewModelBase
    {
        RemoteService remoteService;

        public User EditUser;

        private string companyName;
        private string username;
        private string email;
        private string phoneNumber;
        private string alternatePhoneNumber;
        private string avatar;
        private string address;
        private bool isBusy;

        private DelegateCommand _delegateUpdateUserCommand;
        public DelegateCommand NavigateUpdateUserCommand => _delegateUpdateUserCommand ?? (_delegateUpdateUserCommand = new DelegateCommand(ExecuteUpdateUserCommand));

        

        public string CompanyName
        {
            get { return companyName; }
            set { SetProperty(ref companyName, value); }
        }

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty(ref avatar, value); }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        public string AlternatePhoneNumber
        {
            get { return alternatePhoneNumber; }
            set { SetProperty(ref alternatePhoneNumber, value); }
        }

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public EditProfilePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Edit Profile";
            remoteService = new RemoteService();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("nav"))
            {
                EditUser = parameters.GetValue<User>("nav");
                CompanyName = EditUser.CompanyName;
                Username = EditUser.Username;
                PhoneNumber = EditUser.PhoneNumber;
                Address = EditUser.Address;
                Email = EditUser.Email;
                Avatar = EditUser.Avatar;
                AlternatePhoneNumber = EditUser.AlternatePhoneNumber;
            }
        }

        MaterialSnackbarConfiguration snackBarConfiguration = new MaterialSnackbarConfiguration()
        {
            //MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            MessageTextColor = Color.White.MultiplyAlpha(0.8),
            //TintColor = Color.FromHex("34515E")
            TintColor = Color.Red.MultiplyAlpha(0.8),

        };

        private async void ExecuteUpdateUserCommand()
        {
            //checking for connectivity
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: No internet access", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }


            if (CompanyName == null || Username == null || Email == null || PhoneNumber == null || Address == null)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Fill the required fields", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }



            //IsNotBusy = false;
            var loadingPageConfiguration = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = Color.FromHex("1DA1F2"),
                MessageTextColor = Color.FromHex("FFFFFF"),
                CornerRadius = 10,
                TintColor = Color.FromHex("FFFFFF"),
                ScrimColor = Color.FromHex("1DA1F2").MultiplyAlpha(0.32)
            };

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Processing", configuration: loadingPageConfiguration);
            var updateUser = new User
            {
                CompanyName = this.CompanyName.ToLower().Trim(),
                Username = this.Username.ToLower().Trim(),
                PhoneNumber = this.PhoneNumber.ToLower().Trim(),
                Address = this.Address.ToLower().Trim(),
                Email = this.Email.ToLower().Trim(),
                Avatar = this.Avatar,
                AlternatePhoneNumber = this.AlternatePhoneNumber,
                Password = Settings.PasswordSettings.ToLower()
            };

            var user = await remoteService.UpdateUser(updateUser);



            await loadingDialog.DismissAsync();
            //IsNotBusy = true;
            if (user)
            {
                CompanyName = null;
                Username = null;
                Email = null;
                Address = null;
                PhoneNumber = null;
                AlternatePhoneNumber = null;
                //await App.Current.MainPage.DisplayAlert("SignUp Success", "Congrats your account has been created", "Ok");
                await MaterialDialog.Instance.SnackbarAsync(message: "Profile Updated successfully", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);
                await NavigationService.NavigateAsync("/AppShell");
            }
            else
                await MaterialDialog.Instance.SnackbarAsync(message: "Update failed", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);

        }

    }
}
