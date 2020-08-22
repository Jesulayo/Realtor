using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Realtor.Views
{
    public partial class UploadPage : ContentPage
    {
        public UploadPage()
        {
            InitializeComponent();
        }

        //private async void FrontImage_Tapped(object sender, System.EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

            
        //    bool choice = await DisplayAlert("Confirm", "Where do you want to select your image from", "Camera", "Gallery");

        //    if (choice)
        //    {
        //        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //        {
        //            await DisplayAlert("Error", "Camera not supported", "OK");
        //            return;
        //        }

        //        var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //        {
        //            PhotoSize = PhotoSize.Large
        //        });

        //        if (mediaFile == null)
        //        {
        //            await DisplayAlert("Error", "Camera not supported", "OK");
        //            return;
        //        }


        //        frontImage.Source = ImageSource.FromStream(() =>
        //        {
        //            var stream = mediaFile.GetStream();
        //            mediaFile.Dispose();
        //            return stream;
        //        });
        //    }
        //    else
        //    {

        //        if (!CrossMedia.Current.IsPickPhotoSupported)
        //        {
        //            await DisplayAlert("Error", "Pick photo not supported", "OK");
        //            return;
        //        }

        //        var mediaOptions = new PickMediaOptions()
        //        {
        //            PhotoSize = PhotoSize.Large
        //        };


        //        var file = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

        //        if (file == null)
        //        {
        //            await DisplayAlert("Error", "Pick photo not supported", "OK");
        //            return;
        //        }

        //        frontImage.Source = ImageSource.FromStream(() =>
        //        {
        //            var stream = file.GetStream();
        //            file.Dispose();
        //            return stream;
        //        });
        //    }
        //}


      
    }
}
