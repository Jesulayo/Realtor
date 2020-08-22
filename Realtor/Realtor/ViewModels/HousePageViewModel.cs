using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using Realtor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Realtor.ViewModels
{
    public class HousePageViewModel : ViewModelBase
    {
        RemoteService remoteService;
        
        public ObservableCollection<RealtorProperty> Houses { get; set; }

        private DelegateCommand _delegateHouseSelectionCommand;
        private DelegateCommand _delegateRefreshCommand;
        private DelegateCommand _delegatePreviousPageCommand;

        public DelegateCommand NavigateHouseSelectionCommand => _delegateHouseSelectionCommand ?? (_delegateHouseSelectionCommand = new DelegateCommand(ExecuteHouseSelectionCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(GetHouses));
        public DelegateCommand NavigatePrevoiusPageCommand => _delegatePreviousPageCommand ?? (_delegatePreviousPageCommand = new DelegateCommand(PreviousPage));


        public ObservableCollection<Ads> Adverts { get => GetAdvert(); }
        
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public HousePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "HOUSE";
            Houses = new ObservableCollection<RealtorProperty>();
            remoteService = new RemoteService();
            GetHouses();
        }


        private RealtorProperty houseSelection;
        public RealtorProperty HouseSelection
        {
            get
            {
                return houseSelection;
            }
            set
            {
                SetProperty(ref houseSelection, value);
            }
        }

        
        private async void GetHouses()
        {
            IsBusy = true;
            try
            {
                var houseList = await remoteService.GetAllProperty();
                IsBusy = false;
                Houses.Clear();
                foreach (var item in houseList)
                {
                    if (item.ItemType.ToLower() == "house")
                    {
                        Houses.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    
        private ObservableCollection<Ads> GetAdvert()
        {
            var adsList = new ObservableCollection<Ads>();

            adsList.Add(new Ads
            {
                Image = "https://www.jacklingo.com/realestate/images/listing_photos/DESU152822-302187116091-2019-12-17-11-38-22.jpg",
                Heading = "BUY",
                Description = "We take the emotion out of the process to ensure you are getting the best price for the value"
            });
            adsList.Add(new Ads
            {
                Image = "https://lh6.googleusercontent.com/proxy/Anshwjf1f0D6yqvH4YAbYjT-FJkPmfuVHe6-tgwmNTYT7OaqlwwxPGkwLb7nBjgaT5Ir31Ch8ZSRtDHkZHr3Dro4-OnyxiBK51OMy9uNwpT5fjbKqQ",
                Heading = "RENT",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            adsList.Add(new Ads
            {
                Image = "https://www.jacklingo.com/realestate/images/listing_photos/DESU152822-302187116091-2019-12-17-11-38-22.jpg",
                Heading = "LEASE",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            adsList.Add(new Ads
            {
                Image = "https://www.worldluxuryassociation.org/wp-content/uploads/2016/09/house-for-sale-in-Hyderabad.jpg",
                Heading = "LEASE",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            adsList.Add(new Ads
            {
                Image = "https://lh6.googleusercontent.com/proxy/Anshwjf1f0D6yqvH4YAbYjT-FJkPmfuVHe6-tgwmNTYT7OaqlwwxPGkwLb7nBjgaT5Ir31Ch8ZSRtDHkZHr3Dro4-OnyxiBK51OMy9uNwpT5fjbKqQ",
                Heading = "LEASE",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });

            return adsList;
        }

        private async void ExecuteHouseSelectionCommand()
        {
            if (HouseSelection != null)
            {
                var houseParameter = new NavigationParameters();
                houseParameter.Add("nav", HouseSelection);
                await NavigationService.NavigateAsync("BuyDetailPage", houseParameter);
                HouseSelection = null;
            }
        }

        private void PreviousPage()
        {
            NavigationService.GoBackAsync();
        }
    }
}
