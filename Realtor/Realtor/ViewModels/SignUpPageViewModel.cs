using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using Realtor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Realtor.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        RemoteService remoteService;
        private string companyName;
        private string username;
        private string email;
        private string phoneNumber;
        private string address;
        private string password;
        private string confirmPassword;
        private bool isNotBusy;

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

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }

        

        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set { SetProperty(ref isNotBusy, value); }
        }

        private DelegateCommand _delegateLogInCommand;
        private DelegateCommand _delegateSignUpCommand;

        public DelegateCommand NavigateToLogInCommand => _delegateLogInCommand ?? (_delegateLogInCommand = new DelegateCommand(ExecuteNavigateToLogInCommand));
        public DelegateCommand NavigateToSignUpCommand => _delegateSignUpCommand ?? (_delegateSignUpCommand = new DelegateCommand(ExecuteNavigateToSignUpCommand));

        
        public SignUpPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            remoteService = new RemoteService();
            IsNotBusy = true;
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
            await NavigationService.NavigateAsync("LoginPage");
        }

        private async void ExecuteNavigateToSignUpCommand()
        {
            //checking for connectivity
            var network = Connectivity.NetworkAccess;
            if(network != NetworkAccess.Internet)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: No internet access", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            
            if (CompanyName == null || Username == null || Email == null || PhoneNumber == null || Address == null || Password == null || ConfirmPassword == null)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Fill the required fields", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            if(Password.Length < 4)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Password length must be more than 4 characters", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }
            
            if (Password.ToLower().Trim() != ConfirmPassword.ToLower().Trim())
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Passwords do not match", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);

                return;
            }

            IsNotBusy = false;
            var loadingPageConfiguration = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = Color.FromHex("1DA1F2"),
                MessageTextColor = Color.FromHex("FFFFFF"),
                CornerRadius = 10,
                TintColor = Color.FromHex("FFFFFF"),
                ScrimColor = Color.FromHex("1DA1F2").MultiplyAlpha(0.32)
            };

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Processing", configuration: loadingPageConfiguration);

            var validateUser = await remoteService.ValidateUsername(Username.ToLower().Trim());

            if (!validateUser)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Username already exist", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);
                await loadingDialog.DismissAsync();
                IsNotBusy = true;
                return;
            }

            var signUp = new SignUp
            {
                CompanyName = this.CompanyName.ToLower().Trim(),
                Username = this.Username.ToLower().Trim(),
                Email = this.Email.ToLower().Trim(),
                PhoneNumber = this.PhoneNumber.ToLower().Trim(),
                Address = this.Address.ToLower().Trim(),
                Password = this.Password.ToLower().Trim(),
            };

            var user = await remoteService.SignUpAsync(signUp);

            

            await loadingDialog.DismissAsync();
            IsNotBusy = true;
            if (user)
            {
                CompanyName = null;
                Username = null;
                Email = null;
                Password = null;
                Address = null;
                PhoneNumber = null;
                ConfirmPassword = null;
                //await App.Current.MainPage.DisplayAlert("SignUp Success", "Congrats your account has been created", "Ok");
                await MaterialDialog.Instance.SnackbarAsync(message: "Account created successfully", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);
                await NavigationService.NavigateAsync("LoginPage");
            }
            else
                await MaterialDialog.Instance.SnackbarAsync(message: "Sign up failed", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);

        }
    }
}
