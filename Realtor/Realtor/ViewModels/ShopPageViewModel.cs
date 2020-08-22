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
    public class ShopPageViewModel : ViewModelBase
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
        public ObservableCollection<RealtorProperty> Shops { get; set; }
        private DelegateCommand _delegateShopSelectionCommand;
        private DelegateCommand _delegateRefreshCommand;
        private DelegateCommand _delegatePreviousPageCommand;

        public DelegateCommand NavigateShopSelectionCommand => _delegateShopSelectionCommand ?? (_delegateShopSelectionCommand = new DelegateCommand(ExecuteShopSelectionCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(GetShops));
        public DelegateCommand NavigatePrevoiusPageCommand => _delegatePreviousPageCommand ?? (_delegatePreviousPageCommand = new DelegateCommand(PreviousPage));


        public ObservableCollection<Ads> ShopAdverts { get => GetShopAdvert(); }
        
        public ShopPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "SHOP";
            Shops = new ObservableCollection<RealtorProperty>();
            remoteService = new RemoteService();
            GetShops();
        }

        private RealtorProperty shopSelection;
        public RealtorProperty ShopSelection
        {
            get
            {
                return shopSelection;
            }
            set
            {
                SetProperty(ref shopSelection, value);
            }
        }

        async void GetShops()
        {
            try
            {
                IsBusy = true;

                var landList = await remoteService.GetAllProperty();
                IsBusy = false;
                Shops.Clear();
                foreach (var item in landList)
                {
                    if (item.ItemType.ToLower() == "shop")
                    {
                        Shops.Add(item);
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private ObservableCollection<Ads> GetShopAdvert()
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

        private async void ExecuteShopSelectionCommand()
        {
            if (ShopSelection != null)
            {
                var shopParameter = new NavigationParameters();
                shopParameter.Add("nav", ShopSelection);
                await NavigationService.NavigateAsync("BuyDetailPage", shopParameter);
                ShopSelection = null;
            }
        }

        private void PreviousPage()
        {
            NavigationService.GoBackAsync();
        }
    }
}
