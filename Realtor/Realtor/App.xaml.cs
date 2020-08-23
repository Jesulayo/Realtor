using Plugin.SharedTransitions;
using Prism;
using Prism.Ioc;
using Realtor.Utils;
using Realtor.ViewModels;
using Realtor.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Realtor
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            XF.Material.Forms.Material.Init(this);
            SetMainPage();
            //await NavigationService.NavigateAsync("NavigationPage/OnBoardingPage");
            //await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SharedTransitionNavigationPage>();
            containerRegistry.RegisterForNavigation<AppShell>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<OnBoardingPage, OnBoardingPageViewModel>();
            containerRegistry.RegisterForNavigation<ForgotPasswordPage, ForgotPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<UploadPage, UploadPageViewModel>();
            containerRegistry.RegisterForNavigation<HousePage, HousePageViewModel>();
            containerRegistry.RegisterForNavigation<BuyDetailPage, BuyDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<LandPage, LandPageViewModel>();
            containerRegistry.RegisterForNavigation<ShopPage, ShopPageViewModel>();
            containerRegistry.RegisterForNavigation<MyUploadPage, MyUploadPageViewModel>();
            containerRegistry.RegisterForNavigation<MyUploadDetailPage, MyUploadDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MainSearchPage, MainSearchPageViewModel>();
        }

        public void SetMainPage()
        {
            if (string.IsNullOrEmpty(Settings.UsernameSettings))
            {
                NavigationService.NavigateAsync("NavigationPage/OnBoardingPage");

                //MainPage = new NavigationPage(new LandingPage());
                //  MainPage = new NavigationPage(new CompleteTaskEarnPage());
            }

            else
            {
                NavigationService.NavigateAsync("/AppShell");

            }
        }
    }
}
