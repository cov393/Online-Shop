using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Ventique.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ventique.ViewModels;
using Firebase.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace Ventique.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroductionPage : ContentPage
    {

        public string WebAPIkey = "AIzaSyCpikj8DGg0g6tODudWjKu5_L1F94r7OMk";

        private List<ClientNewsModel> container = new List<ClientNewsModel>
            {
                new ClientNewsModel{IteamName ="Baby Yoda Doll", IteamImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR37NysOk5TkXo_lV3-Xo4OkOaeMbH6MjMWYQ&usqp=CAU", IteamStatus="89.99$"}
            };

        IEnumerable<ClientNewsModel> GetContainers(string searchText = null)
        {
            
            if (string.IsNullOrWhiteSpace(searchText))
                return container;

            return container.Where(c => c.IteamName.StartsWith(searchText));
        }

        public IntroductionPage()
        {
            InitializeComponent();
            //listView.ItemsSource = GetContainers();
            BindingContext = new NewsViewModel();
            GetProfileInformationAndRefreshToken();
        }

        //void listView_Refreshing(object sender, System.EventArgs e)
        //{
        //    listView.ItemsSource = GetContainers();
        //    listView.EndRefresh();
        //}


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = GetContainers(e.NewTextValue);
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Add_Favorites_Clicked(object sender, EventArgs e) {
            await DisplayAlert("Added to Favorites!", "He He", "Ok");
        }

        private async void Remove_Item_Clicked(object sender, EventArgs e) {
            await DisplayAlert("Deleted", "No Dramma", "Ok");

        }

        async void Button_AddProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProducts());
        }

        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));

                var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                MyUsername.Text = savedfirebaseauth.User.Email;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Alert", "Oh no! Token expired", "OK");
            }
        }

        private void Loguot_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new WelcomePage());
        }
    }
}
