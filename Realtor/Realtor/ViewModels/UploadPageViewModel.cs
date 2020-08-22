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
    public class UploadPageViewModel : ViewModelBase
    {
        MediaFile mediaFile1;
        MediaFile mediaFile2;
        MediaFile mediaFile3;
        MediaFile mediaFile4;

        RemoteService remoteService;
        private Image firstImage = new Image();
        private Image secondImage = new Image();
        private Image thirdImage = new Image();
        private Image fourthImage = new Image();


        private string itemName;
        private string location;
        private decimal price;
        private string description;
        private string type;
        private bool negotiable = true;
        private bool isBusy;
        string firstImageUrl;
        string secondImageUrl;
        string thirdImageUrl;
        string fourthImageUrl;

        int image1;
        int image2;
        int image3;
        int image4;


        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                SetProperty(ref itemName, value);
            }
        }
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                SetProperty(ref location, value);
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetProperty(ref price, value);
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetProperty(ref description, value);
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                SetProperty(ref type, value);
            }
        }
        public bool Negotiable
        {
            get
            {
                return negotiable;
            }
            set
            {
                SetProperty(ref negotiable, value);
            }
        }
        


        public Image FirstImage
        {
            get
            {
                return firstImage;
            }
            set
            {
                SetProperty(ref firstImage, value);
            }
        }

        public Image SecondImage
        {
            get
            {
                return secondImage;
            }
            set
            {
                SetProperty(ref secondImage, value);
            }
        }

        public Image ThirdImage
        {
            get
            {
                return thirdImage;
            }
            set
            {
                SetProperty(ref thirdImage, value);
            }
        }

        public Image FourthImage
        {
            get
            {
                return fourthImage;
            }
            set
            {
                SetProperty(ref fourthImage, value);
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }



        private DelegateCommand _delegatefirstImageCommand;
        private DelegateCommand _delegatesecondImageCommand;
        private DelegateCommand _delegatethirdImageCommand;
        private DelegateCommand _delegatefourthImageCommand;
        private DelegateCommand _delegateUploadCommand;

        public DelegateCommand NavigateFirstImageCommand => _delegatefirstImageCommand ?? (_delegatefirstImageCommand = new DelegateCommand(ExecuteNavigateFirstImageCommand));
        public DelegateCommand NavigateSecondImageCommand => _delegatesecondImageCommand ?? (_delegatesecondImageCommand = new DelegateCommand(ExecuteNavigateSecondImageCommand));

        public DelegateCommand NavigateThirdImageCommand => _delegatethirdImageCommand ?? (_delegatethirdImageCommand = new DelegateCommand(ExecuteNavigateThirdImageCommand));

        public DelegateCommand NavigateFourthImageCommand => _delegatefourthImageCommand ?? (_delegatefourthImageCommand = new DelegateCommand(ExecuteNavigateFourthImageCommand));
        public DelegateCommand NavigateUploadCommand => _delegateUploadCommand ?? (_delegateUploadCommand = new DelegateCommand(UploadProperty));

        

        public UploadPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "UPLOAD";
            FirstImage.Source = "Upload.png";
            SecondImage.Source = "Upload.png";
            ThirdImage.Source = "Upload.png";
            FourthImage.Source = "Upload.png";
            IsBusy = true;
            remoteService = new RemoteService();
        }

        private async void ExecuteNavigateFirstImageCommand()
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

                mediaFile1 = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Full
                });

                if (mediaFile1 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }


                FirstImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile1.GetStream();
                    image1 = 1;
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
                    PhotoSize = PhotoSize.Full
                };


                mediaFile1 = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (mediaFile1 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Pick photo not supported", "OK");
                    return;
                }

                FirstImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile1.GetStream();
                    image1 = 1;
                    return stream;
                });
            }
        }

        private async void ExecuteNavigateSecondImageCommand()
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

                mediaFile2 = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Full
                });

                if (mediaFile2 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }


                SecondImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile2.GetStream();
                    image2 = 1;
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
                    PhotoSize = PhotoSize.Full
                };


                mediaFile2 = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (mediaFile2 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Pick photo not supported", "OK");
                    return;
                }

                SecondImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile2.GetStream();
                    image2 = 1;
                    return stream;
                });
            }
        }

        private async void ExecuteNavigateThirdImageCommand()
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

                mediaFile3 = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Full
                });

                if (mediaFile3 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }


                ThirdImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile3.GetStream();
                    image3 = 1;
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
                    PhotoSize = PhotoSize.Full
                };


                mediaFile3 = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (mediaFile3 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Pick photo not supported", "OK");
                    return;
                }

                ThirdImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile3.GetStream();
                    image3 = 1;
                    return stream;
                });
            }
        }

        private async void ExecuteNavigateFourthImageCommand()
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

                mediaFile4 = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Full
                });

                if (mediaFile4 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }


                FourthImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile4.GetStream();
                    image4 = 1;
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
                    PhotoSize = PhotoSize.Full
                };


                mediaFile4 = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (mediaFile4 == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Pick photo not supported", "OK");
                    return;
                }

                FourthImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = mediaFile4.GetStream();
                    image4 = 1;
                    return stream;
                });
            }
        }

        MaterialSnackbarConfiguration snackBarConfiguration = new MaterialSnackbarConfiguration()
        {
            //MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            MessageTextColor = Color.White.MultiplyAlpha(0.8),
            //TintColor = Color.FromHex("34515E")
            TintColor = Color.Red.MultiplyAlpha(0.8),

        };
        async void UploadProperty()
        {
            //checking for connectivity
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: No internet access", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Location) || Price == 0M || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Type))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Fill the required fields", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);
                return;
            }

            IsBusy = false;
            var loadingPageConfiguration = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = Color.FromHex("1DA1F2"),
                MessageTextColor = Color.FromHex("FFFFFF"),
                CornerRadius = 10,
                TintColor = Color.FromHex("FFFFFF"),
                ScrimColor = Color.FromHex("1DA1F2").MultiplyAlpha(0.32)
            };

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Processing", configuration: loadingPageConfiguration);



            if ( image1 == 1)
            {
                firstImageUrl = await remoteService.UploadFile(mediaFile1.GetStream(), Path.GetFileName(mediaFile1.Path));
            }
            
            if(image2 == 1)
            {
                secondImageUrl = await remoteService.UploadFile(mediaFile2.GetStream(), Path.GetFileName(mediaFile2.Path));
            }

            if(image3 == 1)
            {
                thirdImageUrl = await remoteService.UploadFile(mediaFile3.GetStream(), Path.GetFileName(mediaFile3.Path));

            }

            if(image4 == 1)
            {
                fourthImageUrl = await remoteService.UploadFile(mediaFile4.GetStream(), Path.GetFileName(mediaFile4.Path));

            }

            //firstImageUrl = await remoteService.UploadFile(mediaFile1.GetStream(), Path.GetFileName(mediaFile1.Path));
            //secondImage = await remoteService.UploadFile(mediaFile2.GetStream(), Path.GetFileName(mediaFile2.Path));
            //thirdImageUrl = await remoteService.UploadFile(mediaFile3.GetStream(), Path.GetFileName(mediaFile3.Path));
            //fourthImageUrl = await remoteService.UploadFile(mediaFile4.GetStream(), Path.GetFileName(mediaFile4.Path));


            var realtorProperty = new RealtorProperty()
            {
                CompanyName = Settings.CompanyNameSettings,
                AccountName = Settings.UsernameSettings,
                Avatar = Settings.AvatarSettings,
                ItemName = this.ItemName.ToLower().Trim(),
                ItemPrice = this.Price,
                Description = this.Description.ToLower().Trim(),
                Location = this.Location.ToLower().Trim(),
                ItemType = this.Type.ToLower().Trim(),
                Negotiable = this.Negotiable,
                PhoneNumber = Settings.PhoneNumberSettings,
                Email = Settings.EmailSettings,
                FirstImage = firstImageUrl,
                SecondImage = secondImageUrl,
                ThirdImage = thirdImageUrl,
                FourthImage = fourthImageUrl,
                IsFavorite = false

            };

            var upload = await remoteService.AddProperty(realtorProperty);
            await loadingDialog.DismissAsync();
            IsBusy = true;

            if (upload)
            {
                ItemName = null;
                Price = 0;
                Description = null;
                Location = null;
                Type = null;
                Negotiable = false;
                FirstImage.Source = "Upload.png";
                SecondImage.Source = "Upload.png";
                ThirdImage.Source = "Upload.png";
                FourthImage.Source = "Upload.png";
                await MaterialDialog.Instance.SnackbarAsync(message: "Uploaded successfully", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);

            }
            else
                await MaterialDialog.Instance.SnackbarAsync(message: "Uploaded failed", actionButtonText: "OK", msDuration: 3000, configuration: snackBarConfiguration);



            //IsBusy = true;

        }

    }
}
