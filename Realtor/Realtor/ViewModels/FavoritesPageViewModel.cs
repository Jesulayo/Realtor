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
    public class FavoritesPageViewModel : ViewModelBase
    {
        private DelegateCommand _delegateSelectionCommand;
        public DelegateCommand NavigateSelectionCommand => _delegateSelectionCommand ?? (_delegateSelectionCommand = new DelegateCommand(ExecuteSelectionCommand));

        public ObservableCollection<Buy> Buys { get => GetBuyItems(); }

        private Buy buySelection;
        public Buy BuySelection
        {
            get
            {
                return buySelection;
            }
            set
            {
                SetProperty(ref buySelection, value);
            }
        }

        public FavoritesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "FAVORITES";
        }

        private async void ExecuteSelectionCommand()
        {
            if (BuySelection != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("nav", BuySelection);
                await NavigationService.NavigateAsync("BuyDetailPage", parameters);
                BuySelection = null;
            }
        }
        

        private ObservableCollection<Buy> GetBuyItems()
        {
            var buyList = new ObservableCollection<Buy>();
            buyList.Add(new Buy
            {
                AccountName = "Kaysho",
                AccountProfilePicture = "profilepic.jpeg",
                ItemName = "3 bedroom flat",
                ItemPrice = 150000M,
                Location = "Osogbo",
                Negotiable = true,
                ImagePicture = "profilepic.jpeg",
                Description = "2 bedroom + 1 master bedroom \n Sitting room, Dining room \n Kitchen \n 2 toilets + 1 bathroom" +
                "\n Store",
                LongDescription = "This stunning two-story home is on a large lot in a hot neighborhood. From the open-concept kitchen and living space to the large shaded backyard, there is plenty of room for the whole family to enjoy. Recent updates include new carpeting upstairs and stainless appliances. Situated in a family-friendly neighborhood near a great park, this home is sure to go fast!",
                


            });
            buyList.Add(new Buy
            {
                AccountName = "Kaysho",
                AccountProfilePicture = "profilepic.jpeg",
                ItemName = "3 bedroom flat",
                ItemPrice = 150000M,
                Location = "Osogbo",
                Negotiable = true,
                ImagePicture = "profilepic.jpeg",
                Description = "2 bedroom + 1 master bedroom \n Sitting room, Dining room \n Kitchen \n 2 toilets + 1 bathroom" +
                "\n Store",
                LongDescription = "This stunning two-story home is on a large lot in a hot neighborhood. From the open-concept kitchen and living space to the large shaded backyard, there is plenty of room for the whole family to enjoy. Recent updates include new carpeting upstairs and stainless appliances. Situated in a family-friendly neighborhood near a great park, this home is sure to go fast!",
                
            });
            buyList.Add(new Buy
            {
                AccountName = "Kaysho",
                AccountProfilePicture = "profilepic.jpeg",
                ItemName = "3 bedroom flat",
                ItemPrice = 150000M,
                Location = "Osogbo",
                Negotiable = true,
                ImagePicture = "profilepic.jpeg",
                Description = "2 bedroom + 1 master bedroom \n Sitting room, Dining room \n Kitchen \n 2 toilets + 1 bathroom" +
                "\n Store",
                LongDescription = "This stunning two-story home is on a large lot in a hot neighborhood. From the open-concept kitchen and living space to the large shaded backyard, there is plenty of room for the whole family to enjoy. Recent updates include new carpeting upstairs and stainless appliances. Situated in a family-friendly neighborhood near a great park, this home is sure to go fast!",
                
            });
            buyList.Add(new Buy
            {
                AccountName = "Kaysho",
                AccountProfilePicture = "profilepic.jpeg",
                ItemName = "3 bedroom flat",
                ItemPrice = 150000M,
                Location = "Osogbo",
                Negotiable = true,
                ImagePicture = "profilepic.jpeg",
                Description = "2 bedroom + 1 master bedroom \n Sitting room, Dining room \n Kitchen \n 2 toilets + 1 bathroom" +
                "\n Store",
                LongDescription = "This stunning two-story home is on a large lot in a hot neighborhood. From the open-concept kitchen and living space to the large shaded backyard, there is plenty of room for the whole family to enjoy. Recent updates include new carpeting upstairs and stainless appliances. Situated in a family-friendly neighborhood near a great park, this home is sure to go fast!",
                
            });
           


            return buyList;
        }

    }
}
