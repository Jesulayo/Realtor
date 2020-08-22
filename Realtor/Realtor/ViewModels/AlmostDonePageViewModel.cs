using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realtor.Data.Entities;
using Realtor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Realtor.ViewModels
{
    public class AlmostDonePageViewModel : ViewModelBase
    {
        private DelegateCommand _delegateCommandLogIn;
        public DelegateCommand NavigateToLogInCommand => _delegateCommandLogIn ?? (_delegateCommandLogIn = new DelegateCommand(ExecuteNavigateToLogInCommand));

        private ObservableCollection<NigeriaState> NigeriaStates;
        public ObservableCollection<string> state { get => GetStates(); }
        //public ObservableCollection<string> lga = new ObservableCollection<string>();
        public ObservableCollection<string> lgas { get => GetLgas(); }
        //private ObservableCollection<string> _lga;
        //public ObservableCollection<string> lga
        //{
        //    get => GetLgas(); 
        //    set { SetProperty(ref _lga, value); }
             

        //}


        //private DelegateCommand _delegateSelectionCommand;
        //public DelegateCommand NavigateSelectionCommand => _delegateSelectionCommand ?? (_delegateSelectionCommand = new DelegateCommand(ExecuteSelectionCommand));



        private int buySelection = 0;
        public int BuySelection
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

        private void ExecuteSelectionCommand()
        {
            if (state.Count < 0)
            {
                buySelection = 0;
                for (int k = 0; k < NigeriaStates[0].state.locals.Count; k++)
                {
                    lgas.Add(NigeriaStates[0].state.locals[k].name);

                }
            }
            else
            {
                for (int k = 0; k < NigeriaStates[BuySelection].state.locals.Count; k++)
                {
                    lgas.Add(NigeriaStates[BuySelection].state.locals[k].name);

                }
            }

        }


        public AlmostDonePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            ConvertToClass();
            //GetLgas();
        }

        private async void ExecuteNavigateToLogInCommand()
        {
            await NavigationService.NavigateAsync("LoginPage");
        }
        

        private ObservableCollection<string> GetStates()
        {
            var stateList = new ObservableCollection<String>();
            for (int i = 0; i < NigeriaStates.Count; i++)
            {
                stateList.Add(NigeriaStates[i].state.name);
            }
            return stateList;
        }

        private ObservableCollection<string> GetLgas()
        {
            var lgaList = new ObservableCollection<String>();
            
                for (int k = 0; k < NigeriaStates[BuySelection].state.locals.Count; k++)
                {
                    lgaList.Add(NigeriaStates[BuySelection].state.locals[k].name);

                }
                return lgaList;
            
            //else
            //{
            //    for (int k = 0; k < NigeriaStates[BuySelection].state.locals.Count; k++)
            //    {
            //        lgaList.Add(NigeriaStates[BuySelection].state.locals[k].name);

            //    }
            //}

            //for (int i = 0; i < NigeriaStates.Count; i++)
            //{
            //    stateList.Add(NigeriaStates[i].state.name);
            //}

            //return lgaList;

        }

        public void ConvertToClass()
        {
            string jsonFileName = "state.json";
            var assembly = typeof(AlmostDonePage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            //Stream stream = assembly.GetManifestResourceStream("Realtor.state.json");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                NigeriaStates = JsonConvert.DeserializeObject<ObservableCollection<NigeriaState>>(jsonString);
            }

            

        }

        public void OnChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            //List<String> lga = new List<string>();


            for (int k = 0; k < NigeriaStates[selectedIndex].state.locals.Count; k++)
            {
                lgas.Add(NigeriaStates[selectedIndex].state.locals[k].name);

            }

        }

        //public void ConvertToClass()
        //{
        //    //string jsonFileName = "state.json";
        //    var assembly = typeof(AlmostDonePageViewModel).GetTypeInfo().Assembly;
        //    //Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
        //    Stream stream = assembly.GetManifestResourceStream("Realtor.state.json");
        //    using (var reader = new StreamReader(stream))
        //    {
        //        var jsonString = reader.ReadToEnd();
        //        NigeriaStates = JsonConvert.DeserializeObject<ObservableCollection<NigeriaState>>(jsonString);
        //    }

        //}
    }
}
