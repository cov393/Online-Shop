using Ventique.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ventique {
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            {
                MainPage = new NavigationPage(new IntroductionPage());
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
