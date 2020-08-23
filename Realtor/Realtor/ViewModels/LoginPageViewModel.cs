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
    public class LoginPageViewModel : ViewModelBase
    {
        RemoteService remoteService;
        private string username;
        private string password;
        private bool isBusy;
        private bool isNotBusy = true;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set { SetProperty(ref isNotBusy, value); }
        }



        private DelegateCommand _delegateCommandForgotPassword;
        private DelegateCommand _delegateCommandSignUp;
        private DelegateCommand _delegateCommandLogIn;

        public DelegateCommand NavigateToForgotPasswordCommand => _delegateCommandForgotPassword ?? (_delegateCommandForgotPassword = new DelegateCommand(ExecuteNavigateToForgotPasswordCommand));
        public DelegateCommand NavigateToSignUpCommand => _delegateCommandSignUp ?? (_delegateCommandSignUp = new DelegateCommand(ExecuteNavigateToSignUpCommand));
        public DelegateCommand NavigateToLogInCommand => _delegateCommandLogIn ?? (_delegateCommandLogIn = new DelegateCommand(ExecuteNavigateToLogInCommand));

        

        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            //BackgroundOpacity = 1;
            remoteService = new RemoteService();
        }

        MaterialSnackbarConfiguration snackBarConfiguration = new MaterialSnackbarConfiguration()
        {
            //MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            MessageTextColor = Color.White.MultiplyAlpha(0.8),
            //TintColor = Color.FromHex("34515E")
            TintColor = Color.Red.MultiplyAlpha(0.8),

        };

        private async void ExecuteNavigateToLogInCommand()
        {
            //checking for connectivity
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: No internet access", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Fill the required fields", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            IsBusy = true;
            IsNotBusy = false;
            var login = new Login()
            {
                Username = this.Username.ToLower().Trim(),
                Password = this.Password.ToLower().Trim()
            };

            var user = await remoteService.LoginAsync(login);

            IsBusy = false;
            IsNotBusy = true;
            if (user != null)
            {
                Settings.CompanyNameSettings = user.CompanyName;
                Settings.UsernameSettings = user.Username;
                Settings.EmailSettings = user.Email;
                Settings.AlternatePhoneNumberSettings = user.AlternatePhoneNumber;
                Settings.AvatarSettings = user.Avatar;
                Settings.PhoneNumberSettings = user.PhoneNumber;
                Settings.AddressSettings = user.Address;
                Settings.PasswordSettings = user.Password;
                await NavigationService.NavigateAsync("/AppShell");
                
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Account not found", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            
        }
        private void ExecuteNavigateToSignUpCommand()
        {
            NavigationService.NavigateAsync("SignUpPage");
        }

        private void ExecuteNavigateToForgotPasswordCommand()
        {
            NavigationService.NavigateAsync("ForgotPasswordPage");
        }
    }
}
