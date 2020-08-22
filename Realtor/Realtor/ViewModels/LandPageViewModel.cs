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
    public class LandPageViewModel : ViewModelBase
    {
        RemoteService remoteService;

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                SetProperty(ref isBusy, value);
            }
        }

        public ObservableCollection<RealtorProperty> Lands { get; set; }

        private DelegateCommand _delegateLandSelectionCommand;
        private DelegateCommand _delegateRefreshCommand;
        private DelegateCommand _delegatePreviousPageCommand;

        public DelegateCommand NavigateLandSelectionCommand => _delegateLandSelectionCommand ?? (_delegateLandSelectionCommand = new DelegateCommand(ExecuteLandSelectionCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(GetLands));
        public DelegateCommand NavigatePrevoiusPageCommand => _delegatePreviousPageCommand ?? (_delegatePreviousPageCommand = new DelegateCommand(PreviousPage));


        public ObservableCollection<Ads> LandAdverts { get => GetLandAdvert(); }
        
        public LandPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "LAND";
            Lands = new ObservableCollection<RealtorProperty>();
            remoteService = new RemoteService();
            GetLands();
        }

        private RealtorProperty landSelection;
        public RealtorProperty LandSelection
        {
            get
            {
                return landSelection;
            }
            set
            {
                SetProperty(ref landSelection, value);
            }
        }

        async void GetLands()
        {
            try
            {
                IsBusy = true;

                var landList = await remoteService.GetAllProperty();
                IsBusy = false;
                Lands.Clear();
                foreach (var item in landList)
                {
                    if(item.ItemType.ToLower() == "land")
                    {
                        Lands.Add(item);
                    }

                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private ObservableCollection<Ads> GetLandAdvert()
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

        private async void ExecuteLandSelectionCommand()
        {
            if (LandSelection != null)
            {
                var landParameter = new NavigationParameters();
                landParameter.Add("nav", LandSelection);
                await NavigationService.NavigateAsync("BuyDetailPage", landParameter);
                LandSelection = null;
            }
        }

        private void PreviousPage()
        {
            NavigationService.GoBackAsync();
        }
    }
}
