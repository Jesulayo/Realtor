using Plugin.Media;
using Plugin.Media.Abstractions;
using Realtor.Services;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace Realtor.Views
{
    public partial class UploadPicturePage : ContentPage
    {
        MediaFile file;
        RemoteService remoteService;
        public UploadPicturePage()
        {
            InitializeComponent();
            remoteService = new RemoteService();
        }

        private async void BtnPick_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            await remoteService.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
        }

        private void BtnDownload_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
    
        }
    }
}
