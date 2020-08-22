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
    public class SearchPageViewModel : ViewModelBase
    {
        RemoteService remoteService;
        private DelegateCommand _delegateHouseSelectionCommand;
        private DelegateCommand _delegateLandSelectionCommand;
        private DelegateCommand _delegateShopSelectionCommand;
        private DelegateCommand _delegateRefreshCommand;
        private DelegateCommand _delegateSearchCommand;

        public DelegateCommand NavigateHouseSelectionCommand => _delegateHouseSelectionCommand ?? (_delegateHouseSelectionCommand = new DelegateCommand(ExecuteHouseSelectionCommand));
        public DelegateCommand NavigateLandSelectionCommand => _delegateLandSelectionCommand ?? (_delegateLandSelectionCommand = new DelegateCommand(ExecuteLandSelectionCommand));
        public DelegateCommand NavigateShopSelectionCommand => _delegateShopSelectionCommand ?? (_delegateShopSelectionCommand = new DelegateCommand(ExecuteShopSelectionCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(GetAllProperty));
        public DelegateCommand NavigateSearchCommand => _delegateSearchCommand ?? (_delegateSearchCommand = new DelegateCommand(ExecuteSearchCommand));

        
        private DelegateCommand _delegateSeeAllHouse;
        private DelegateCommand _delegateSeeAllLand;
        private DelegateCommand _delegateSeeAllShop;


        public DelegateCommand NavigateToSeeAllHouseCommand => _delegateSeeAllHouse ?? (_delegateSeeAllHouse = new DelegateCommand(ExecuteNavigateToSeeAllHouseCommand));
        public DelegateCommand NavigateToSeeAllLandCommand => _delegateSeeAllLand ?? (_delegateSeeAllLand = new DelegateCommand(ExecuteNavigateToSeeAllLandCommand));
        public DelegateCommand NavigateToSeeAllShopCommand => _delegateSeeAllShop ?? (_delegateSeeAllShop = new DelegateCommand(ExecuteNavigateToSeeAllShopCommand));

        public ObservableCollection<RealtorProperty> Houses { get; set; }

        public ObservableCollection<RealtorProperty> Lands { get; set; }
        public ObservableCollection<RealtorProperty> Shops { get; set; }

        

        //public ObservableCollection<Welcome> Welcomes { get => GetWelcomes(); }


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

        

        public SearchPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Lands = new ObservableCollection<RealtorProperty>();
            Shops = new ObservableCollection<RealtorProperty>();
            Houses = new ObservableCollection<RealtorProperty>();
            remoteService = new RemoteService();
            GetAllProperty();
        }

        void GetAllProperty()
        {
            GetHouses();
            GetLands();
            GetShops();
        }

        async void GetHouses()
        {
            //IsBusy = true;
            try
            {
                var houseList = await remoteService.GetAllProperty();
                Houses.Clear();
                foreach (var item in houseList)
                {
                    if (item.ItemType.ToLower() == "house")
                    {
                        if (Houses.Count < 3)
                        {
                            Houses.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        async void GetLands()
        {
            //IsBusy = true;
            try
            {
                Lands.Clear();
                var landList = await remoteService.GetAllProperty();
                foreach (var item in landList)
                {
                    if (item.ItemType.ToLower() == "land")
                    {
                        if (Lands.Count < 3)
                        {
                            Lands.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        async void GetShops()
        {
            //IsBusy = true;
            try
            {
                Shops.Clear();
                var shopList = await remoteService.GetAllProperty();
                foreach (var item in shopList)
                {
                    if (item.ItemType.ToLower() == "shop")
                    {
                        if (Shops.Count < 3)
                        {
                            Shops.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void ExecuteNavigateToSeeAllHouseCommand()
        {
            NavigationService.NavigateAsync("HousePage");
        }

        private void ExecuteNavigateToSeeAllLandCommand()
        {
            NavigationService.NavigateAsync("LandPage");
        }

        private void ExecuteNavigateToSeeAllShopCommand()
        {
            NavigationService.NavigateAsync("ShopPage");
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

        private async void ExecuteSearchCommand()
        {
            await NavigationService.NavigateAsync("NavigationPage/MainSearchPage");
        }


    }
}
