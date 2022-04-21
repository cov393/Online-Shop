using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventique.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUpPage : ContentPage
    {
        public string WebAPIkey = "AIzaSyCpikj8DGg0g6tODudWjKu5_L1F94r7OMk";
        public SingUpPage()
        {
            InitializeComponent();
        }
        async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(UserNewEmail.Text, UserNewPassword.Text);
                string gettoken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Alert", "Congratulations to our new user", "OK");
                await Navigation.PushAsync(new LoginPage());

            }
            catch (Exception )
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid Email or Password too weak", "OK");
            }
        }
    }
}