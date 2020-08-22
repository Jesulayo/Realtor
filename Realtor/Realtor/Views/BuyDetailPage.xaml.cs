using System.Threading;
using Xamarin.Forms;

namespace Realtor.Views
{
    public partial class BuyDetailPage : ContentPage
    {
        //private bool _isContactOpened;
        //private CancellationTokenSource _tokenSource;


        public BuyDetailPage()
        {
            InitializeComponent();
        }

        //private void OnContactClicked(VisualElement sender, TouchEffect.EventArgs.TouchCompletedEventArgs args)
        //{
        //    _tokenSource?.Cancel();
        //    _tokenSource = new CancellationTokenSource();
        //    var token = _tokenSource.Token;
        //    _isContactOpened = !_isContactOpened;
        //    var firstSocial = FirstContact;
        //    var secondSocial = SecondContact;
        //    if (_isContactOpened)
        //    {
        //        firstSocial = SecondContact;
        //        secondSocial = FirstContact;
        //    }

        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await firstSocial.ScaleTo(_isContactOpened ? 1 : 0);
        //        if (token.IsCancellationRequested) return;
        //        await secondSocial.ScaleTo(_isContactOpened ? 1 : 0);
        //    });
        //}
    }
}
