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
    public class HouseDetailPageViewModel : ViewModelBase
    {
        private RealtorProperty HouseItems;
        private string itemName;
        private string imagePicture;
        private decimal itemPrice;
        private string description;
        private string location;
        private bool negotiable;
        private string seller;
        private string sellerContact;
        private string avatar;
        private string sellerPhoneNumber;
        private string sellerEmailAddress;

        public string ItemName
        {
            get { return itemName; }
            set { SetProperty(ref itemName, value); }
        }
        public string Avatar
        {
            get { return avatar; }
            set { SetProperty(ref avatar, value); }
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

        public string SellerPhoneNumber
        {
            get { return sellerPhoneNumber; }
            set { SetProperty(ref sellerPhoneNumber, value); }
        }
        public string SellerEmailAddress
        {
            get { return sellerEmailAddress; }
            set { SetProperty(ref sellerEmailAddress, value); }
        }

        public HouseDetailPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("houseNav"))
            {
                HouseItems = parameters.GetValue<RealtorProperty>("houseNav");
                Seller = HouseItems.CompanyName;
                Avatar = HouseItems.Avatar;
                ItemName = HouseItems.ItemName;
                ImagePicture = HouseItems.FirstImage;
                ItemPrice = HouseItems.ItemPrice;
                Description = HouseItems.Description;
                Location = HouseItems.Location;
                Negotiable = HouseItems.Negotiable;
                SellerPhoneNumber = HouseItems.PhoneNumber;
                SellerEmailAddress = HouseItems.Email;
            }
        }

    }
}
