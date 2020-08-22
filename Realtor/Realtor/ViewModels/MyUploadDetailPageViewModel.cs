using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;

namespace Realtor.ViewModels
{
    public class MyUploadDetailPageViewModel : ViewModelBase
    {
        public RealtorProperty MyUploadItems;
        private string itemName;
        private string imagePicture;
        private decimal itemPrice;
        private string description;
        private string location;
        private bool negotiable;
        private string seller;

        private DelegateCommand _delegateCallCommand;
        private DelegateCommand _delegateEmailCommand;
        private string avatar;
        private string sellerPhoneNumber;
        private string sellerEmailAddress;

        public DelegateCommand NavigateCallCommand => _delegateCallCommand ?? (_delegateCallCommand = new DelegateCommand(PlacePhoneCall));
        public DelegateCommand NavigateEmailCommand => _delegateEmailCommand ?? (_delegateEmailCommand = new DelegateCommand(SendEmail));


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

        public MyUploadDetailPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }

        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("nav"))
            {
                MyUploadItems = parameters.GetValue<RealtorProperty>("nav");
                Seller = MyUploadItems.CompanyName;
                Avatar = MyUploadItems.Avatar;
                ItemName = MyUploadItems.ItemName;
                ImagePicture = MyUploadItems.FirstImage;
                ItemPrice = MyUploadItems.ItemPrice;
                Description = MyUploadItems.Description;
                Location = MyUploadItems.Location;
                Negotiable = MyUploadItems.Negotiable;
                SellerPhoneNumber = MyUploadItems.PhoneNumber;
                SellerEmailAddress = MyUploadItems.Email;
            }
        }

        private void PlacePhoneCall()
        {
            try
            {
                PhoneDialer.Open(SellerPhoneNumber);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void SendEmail()
        {
            try
            {
                await Email.ComposeAsync("", "", SellerEmailAddress);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
