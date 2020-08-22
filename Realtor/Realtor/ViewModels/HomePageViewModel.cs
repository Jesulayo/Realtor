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
using System.Threading.Tasks;

namespace Realtor.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<RealtorProperty> RealtorData { get; set; }

        RemoteService remoteService;
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private RealtorProperty realtorDataSelection;
        public RealtorProperty RealtorDataSelection
        {
            get
            {
                return realtorDataSelection;
            }
            set
            {
                SetProperty(ref realtorDataSelection, value);
            }

        }

        private DelegateCommand _delegateSelectionCommand;
        private DelegateCommand _delegateRefreshCommand;
        public DelegateCommand NavigateSelectionCommand => _delegateSelectionCommand ?? (_delegateSelectionCommand = new DelegateCommand(ExecuteSelectionCommand));
        public DelegateCommand NavigateRefreshCommand => _delegateRefreshCommand ?? (_delegateRefreshCommand = new DelegateCommand(GetAllItems));


        

        public HomePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = "HOME";
            RealtorData = new ObservableCollection<RealtorProperty>();
            remoteService = new RemoteService();
            GetAllItems();
        }

       
        private async void ExecuteSelectionCommand()
        {
            if (RealtorDataSelection != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("nav", RealtorDataSelection);
                await NavigationService.NavigateAsync("BuyDetailPage", parameters);
                RealtorDataSelection = null;
            }
        }

        private async void GetAllItems()
        {
            IsBusy = true;
            try
            {
                var realtorProperty =  await remoteService.GetAllProperty();
                IsBusy = false;
                RealtorData.Clear();
                //for (int i = 0; i > realtorProperty.Count; i++)
                //{
                //    RealtorData.Add(realtorProperty[i]);

                //}


                foreach (var item in realtorProperty)
                {
                    RealtorData.Add(item);
                }

                RealtorData.Reverse();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        
    }
}
