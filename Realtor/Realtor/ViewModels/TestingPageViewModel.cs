using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Realtor.ViewModels
{
    public class TestingPageViewModel : ViewModelBase
    {
        private string image;
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        public Buy buy;
        public TestingPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("nav"))
            {
                buy = parameters.GetValue<Buy>("nav");
                Title = buy.AccountName;
                Image = buy.AccountProfilePicture;
            }
            
        }
    }
}
