﻿using System;
using System.Timers;
using Xamarin.Forms;

namespace Realtor.Views
{
    public partial class HousePage : ContentPage
    {
        public HousePage()
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
            Device.BeginInvokeOnMainThread( () =>
            {
                if (cvHouse.Position == 4)
                {
                    cvHouse.Position = 0;
                    
                    return;
                }

                cvHouse.Position += 1;

            });
        }
    }
}
