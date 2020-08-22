using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using Realtor.Services;
using Realtor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Realtor.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        RemoteService remoteService;
        MediaFile mediaFile;
        int number;
        string profilePicUrl;
        private string companyName;
        private string username;
        private string email;
        private string phoneNumber;
        private string alternatePhoneNumber;
        private Image avatar = new Image();
        private string address;
        private bool isBusy;
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

        public Image Avatar
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

        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set { SetProperty(ref isNotBusy, value); }
        }


        private DelegateCommand _delegateMyUploadCommand;
        private DelegateCommand _delegateEditProfileCommand;
        private DelegateCommand _delegateLogoutCommand;
        private DelegateCommand _delegateRefreshCommand;
        private DelegateCommand _delegateUpdateUserCommand;
        private DelegateCommand _delegateProfilePictureCommand;


        public DelegateCommand NavigateMyUploadCommand => _delegateMyUploadCommand ?? (_delegateMyUploadCommand = new DelegateCommand(ExecuteMyUploadCommand));
        public DelegateCommand NavigateLogoutCommand => _delegateLogoutCommand ?? (_delegateLogoutCommand = new DelegateCommand(ExecuteLogoutCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(LoadProfile));
        public DelegateCommand NavigateEditProfileCommand => _delegateEditProfileCommand ?? (_delegateEditProfileCommand = new DelegateCommand(ExecuteEditProfileCommand));
        public DelegateCommand NavigateUpdateUserCommand => _delegateUpdateUserCommand ?? (_delegateUpdateUserCommand = new DelegateCommand(ExecuteUpdateUserCommand));
        public DelegateCommand NavigateProfilePictureCommand => _delegateProfilePictureCommand ?? (_delegateProfilePictureCommand = new DelegateCommand(ExecuteProfilePictureCommand));

        

        public ProfilePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "PROFILE";
            remoteService = new RemoteService();
            LoadProfile();
        }

        private void ExecuteMyUploadCommand()
        {
            NavigationService.NavigateAsync("NavigationPage/MyUploadPage");
        }

        MaterialSnackbarConfiguration snackBarConfiguration = new MaterialSnackbarConfiguration()
        {
            //MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            MessageTextColor = Color.White.MultiplyAlpha(0.8),
            //TintColor = Color.FromHex("34515E")
            TintColor = Color.Red.MultiplyAlpha(0.8),

        };

        async void LoadProfile()
        {
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: No internet access", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }
            IsBusy = true;
            Username = Settings.UsernameSettings;

            var user = await remoteService.GetUserProfile(Username);
            IsBusy = false;

            if(user != null)
            {
                CompanyName = user.CompanyName;
                Username = user.Username;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
                Email = user.Email;
                Avatar.Source = user.Avatar;
                AlternatePhoneNumber = user.AlternatePhoneNumber;
            }
            else
            {
                Username = null;
            }

        }

        private async void ExecuteProfilePictureCommand()
        {
            await CrossMedia.Current.Initialize();


            bool choice = await App.Current.MainPage.DisplayAlert("Confirm", "Where do you want to select your image from", "Camera", "Gallery");

            if (choice)
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }

                mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small
                });

                if (mediaFile == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }


                Avatar.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile.GetStream();
                    number = 1;
                    return stream;
                });
            }
            else
            {

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Pick photo not supported", "OK");
                    return;
                }

                var mediaOptions = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Large
                };


                mediaFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (mediaFile == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Pick photo not supported", "OK");
                    return;
                }

                Avatar.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile.GetStream();
                    number = 1;
                    return stream;
                });
            }
        }
        private void ExecuteEditProfileCommand()
        {
            IsNotBusy = true;
        }

        private async void ExecuteUpdateUserCommand()
        {
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

            var loadingPageConfiguration = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = Color.FromHex("1DA1F2"),
                MessageTextColor = Color.FromHex("FFFFFF"),
                CornerRadius = 10,
                TintColor = Color.FromHex("FFFFFF"),
                ScrimColor = Color.FromHex("1DA1F2").MultiplyAlpha(0.32)
            };

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Processing", configuration: loadingPageConfiguration);

            if (number == 1)
            {
                profilePicUrl = await remoteService.UploadFile(mediaFile.GetStream(), Path.GetFileName(mediaFile.Path));
            }

            if(number != 1)
            {
                profilePicUrl = Avatar.Source.ToString();
            }

            var updateUser = new User
            {
                CompanyName = this.CompanyName.ToLower().Trim(),
                Username = this.Username.ToLower().Trim(),
                PhoneNumber = this.PhoneNumber.ToLower().Trim(),
                Address = this.Address.ToLower().Trim(),
                Email = this.Email.ToLower().Trim(),
                Avatar = profilePicUrl,
                AlternatePhoneNumber = this.AlternatePhoneNumber,
                Password = Settings.PasswordSettings.ToLower()
            };

            var user = await remoteService.UpdateUser(updateUser);



            await loadingDialog.DismissAsync();
            //IsNotBusy = true;
            if (user)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Profile Updated successfully", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);
                IsNotBusy = false;
            }
            else
                await MaterialDialog.Instance.SnackbarAsync(message: "Update failed", actionButtonText: "OK", msDuration: 2000, configuration: snackBarConfiguration);


            //var parameters = new NavigationParameters();
            //parameters.Add("nav", new User()
            //{
            //    CompanyName = this.CompanyName,
            //    Username = this.username,
            //    PhoneNumber = this.PhoneNumber,
            //    Address = this.Address,
            //    Email = this.Email,
            //    Avatar = this.Avatar,
            //    AlternatePhoneNumber = this.AlternatePhoneNumber
            //    });


            //await NavigationService.NavigateAsync("NavigationPage/EditProfilePage", parameters);

        }

        private async void ExecuteLogoutCommand()
        {
            Settings.ClearAllData();
            await NavigationService.NavigateAsync("/LoginPage");
        }
    }
}
