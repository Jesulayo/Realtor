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
                Image = "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/home-selling-video-ad-template-design-ee5e8cf8367855aba51de46a841e14ae_screen.jpg?ts=1567091200",
                Heading = "BUY",
                Description = "We take the emotion out of the process to ensure you are getting the best price for the value"
            });
            adsList.Add(new Ads
            {
                Image = "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/real-estate-property-selling-video-ad-template-86bc96a8e4d57e1fe5b2e92361b0b4a5_screen.jpg?ts=1567091543",
                Heading = "RENT",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            adsList.Add(new Ads
            {
                Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.postermywall.com%2Findex.php%2Fposterbuilder%2Fcopy%2F9f457019a8bbf150c6adeff93418f0f2&psig=AOvVaw0Tl5FkkBJ0VkwPsNaVqpSd&ust=1614100936578000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCJj4u9mA_u4CFQAAAAAdAAAAABBD",
                Heading = "LEASE",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            adsList.Add(new Ads
            {
                Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.postermywall.com%2Findex.php%2Fart%2Ftemplate%2F2071b5ead46395f9b8fffc785ad5c1c3%2Fopen-house-real-estate-signage-ad-design-template&psig=AOvVaw0Tl5FkkBJ0VkwPsNaVqpSd&ust=1614100936578000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCJj4u9mA_u4CFQAAAAAdAAAAABBT",
                Heading = "LEASE",
                Description = "Explore the world from your point of view. Visit mountain and enjoy the freshness of life"
            });
            adsList.Add(new Ads
            {
                Image = "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/home-selling-video-ad-template-design-ee5e8cf8367855aba51de46a841e14ae_screen.jpg?ts=1567091200",
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
