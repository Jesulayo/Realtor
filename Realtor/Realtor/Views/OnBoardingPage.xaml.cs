using Prism.Navigation.Xaml;
using System;
using System.Timers;
using Xamarin.Forms;

namespace Realtor.Views
{
    public partial class OnBoardingPage : ContentPage
    {
        public OnBoardingPage()
        {
            InitializeComponent();
        }
        private Timer timer;

        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (cvWalk.Position == 2)
                {
                    //cvWalkthrough.Position = 0;
                    await Navigation.PushAsync(new LoginPage());
                    
                    //return;
                }

                cvWalk.Position += 1;

            });
        }

    }
}
