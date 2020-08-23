using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Realtor.ViewModels
{
    public class BuyDetailPageViewModel : ViewModelBase
    {
        private DelegateCommand _delegateCommandSignUp;
        private DelegateCommand _delegateCallCommand;
        private DelegateCommand _delegateEmailCommand;
        private DelegateCommand _delegatePreviousPageCommand;


        public DelegateCommand NavigateToSignUpCommand => _delegateCommandSignUp ?? (_delegateCommandSignUp = new DelegateCommand(ExecuteNavigateToSignUpCommand));
        public DelegateCommand NavigateCallCommand => _delegateCallCommand ?? (_delegateCallCommand = new DelegateCommand(PlacePhoneCall));
        public DelegateCommand NavigateEmailCommand => _delegateEmailCommand ?? (_delegateEmailCommand = new DelegateCommand(SendEmail));
        public DelegateCommand NavigatePrevoiusPageCommand => _delegatePreviousPageCommand ?? (_delegatePreviousPageCommand = new DelegateCommand(PreviousPage));

     
        private void ExecuteNavigateToSignUpCommand()
        {
            NavigationService.NavigateAsync("SignUpPage");
        }


        public RealtorProperty BuyItems;
        private string avatar;
        private string itemName;
        private string imagePicture;
        private decimal itemPrice;
        private string description;
        private string location;
        private bool negotiable;
        private string seller;
        private string sellerPhoneNumber;
        private string sellerEmailAddress;

        

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty(ref avatar, value); }
        }


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

        public BuyDetailPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("nav"))
            {
                BuyItems = parameters.GetValue<RealtorProperty>("nav");
                Seller = BuyItems.CompanyName;
                Avatar = BuyItems.Avatar;
                ItemName = BuyItems.ItemName;
                ImagePicture = BuyItems.FirstImage;
                ItemPrice = BuyItems.ItemPrice;
                Description = BuyItems.Description;
                Location = BuyItems.Location;
                Negotiable = BuyItems.Negotiable;
                SellerPhoneNumber = BuyItems.PhoneNumber;
                SellerEmailAddress = BuyItems.Email;
            }
        }


        private void PlacePhoneCall()
        {
            try
            {
                PhoneDialer.Open(SellerPhoneNumber);
            }
            catch(Exception ex)
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

        private void PreviousPage()
        {
            NavigationService.GoBackAsync();
        }
    }
}
