using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Models;
using Realtor.Services;
using Realtor.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Realtor.ViewModels
{
    public class MyUploadPageViewModel : ViewModelBase
    {
        RemoteService remoteService;
        public ObservableCollection<RealtorProperty> MyUploads { get; set; }
        private DelegateCommand _delegateMyUploadSelectionCommand;
        private DelegateCommand _delegateRefreshCommand;
        private DelegateCommand _delegatePreviousPageCommand;

        public DelegateCommand NavigateMyUploadSelectionCommand => _delegateMyUploadSelectionCommand ?? (_delegateMyUploadSelectionCommand = new DelegateCommand(ExecuteSelectionCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(GetMyUploads));
        public DelegateCommand NavigatePrevoiusPageCommand => _delegatePreviousPageCommand ?? (_delegatePreviousPageCommand = new DelegateCommand(PreviousPage));


        //public ObservableCollection<Buy> MyUploads { get => GetMyUploads(); }
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public MyUploadPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "MY UPLOADS";
            remoteService = new RemoteService();
            MyUploads = new ObservableCollection<RealtorProperty>();
            GetMyUploads();
        }

        

        private RealtorProperty myUploadSelection;
        public RealtorProperty MyUploadSelection
        {
            get
            {
                return myUploadSelection;
            }
            set
            {
                SetProperty(ref myUploadSelection, value);
            }
        }

        async void GetMyUploads()
        {
            IsBusy = true;
            try
            {
                var realtorProperty = await remoteService.GetAllProperty();
                IsBusy = false;
                MyUploads.Clear();

                foreach (var item in realtorProperty)
                {
                    if(item.AccountName == Settings.UsernameSettings)
                    {
                        MyUploads.Add(item);

                    }
                }

                //int a = MyUploads.Count;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void ExecuteSelectionCommand()
        {
            if (MyUploadSelection != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("nav", MyUploadSelection);
                await NavigationService.NavigateAsync("BuyDetailPage", parameters);
                MyUploadSelection = null;
            }
        }

        private void PreviousPage()
        {
            NavigationService.GoBackAsync();
        }
       
    }
}
