using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Realtor.ViewModels
{
    public class ShopDetailPageViewModel : ViewModelBase
    {
        private RealtorProperty ShopItems;
        private string itemName;
        private string imagePicture;
        private decimal itemPrice;
        private string description;
        private string longDescription;
        private string location;
        private bool negotiable;
        private string seller;
        private string sellerContact;

        public string ItemName
        {
            get { return itemName; }
            set { SetProperty(ref itemName, value); }
        }
        public string ImagePicture
        {
            get { return imagePicture; }
            set { SetProperty(ref imagePicture, value); }
        }
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { SetProperty(ref itemPrice, value); }
        }
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public string LongDescription
        {
            get { return longDescription; }
            set { SetProperty(ref longDescription, value); }
        }
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }
        public bool Negotiable
        {
            get { return negotiable; }
            set { SetProperty(ref negotiable, value); }
        }
        public string Seller
        {
            get { return seller; }
            set { SetProperty(ref seller, value); }
        }
        public string SellerContact
        {
            get { return sellerContact; }
            set { SetProperty(ref sellerContact, value); }
        }
        public ShopDetailPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("shopNav"))
            {
                ShopItems = parameters.GetValue<RealtorProperty>("shopNav");
                ItemName = ShopItems.ItemName;
                ImagePicture = ShopItems.FirstImage;
                ItemPrice = ShopItems.ItemPrice;
                Description = ShopItems.Description;
                Location = ShopItems.Location;
                Negotiable = ShopItems.Negotiable;
                Seller = ShopItems.AccountName;
                SellerContact = "08136982270";
            }
        }
    }
}
