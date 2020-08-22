using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Realtor.ViewModels
{
    public class OnBoardingPageViewModel : ViewModelBase
    {
        private DelegateCommand _delegateCommandLogin;

        public DelegateCommand NavigateToLoginCommand => _delegateCommandLogin ?? (_delegateCommandLogin = new DelegateCommand(ExecuteNavigateToLoginCommand));

        public ObservableCollection<Welcome> Welcomes { get => GetWelcomes(); }

        public OnBoardingPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
        async private void ExecuteNavigateToLoginCommand()
        {
           await NavigationService.NavigateAsync("/LoginPage");
        }
        private ObservableCollection<Welcome> GetWelcomes()
        {
            var welcomeList = new ObservableCollection<Welcome>();

            welcomeList.Add(new Welcome
            {
                Image = "whitelogo.png",
                Heading = "BUY",
                Description = "We take the emotion out of the process to ensure you are getting the best price for the value"
            });
            welcomeList.Add(new Welcome
            {
                Image = "whitelogo.png",
                Heading = "RENT",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            welcomeList.Add(new Welcome
            {
                Image = "whitelogo.png",
                Heading = "LEASE",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            return welcomeList;
        }


    }
}
