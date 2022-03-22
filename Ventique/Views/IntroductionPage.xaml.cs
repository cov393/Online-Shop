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

namespace Ventique
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroductionPage : ContentPage
    {
        IEnumerable<Containers> GetContainers(string searchText = null)
        {
            var container = new List<Containers>
            {
                new Containers {Name ="Product 1", 
                                ImageUrl="https://i.pinimg.com/564x/80/66/45/806645e0dd219bd0ae881c0fdcdb2e58.jpg", Status="$45"},
                new Containers {Name ="Product 2", 
                                ImageUrl="https://i.pinimg.com/564x/06/03/0f/06030f9ffaee33695f27c6b87bf151e3.jpg", Status="$45"}
            };

            if (string.IsNullOrWhiteSpace(searchText))
                return container;

            return container.Where(c => c.Name.StartsWith(searchText));
        }
        public IntroductionPage()
        {
            //InitializeComponent();
            listView.EndRefresh();

            listView.ItemsSource = GetContainers();

        }

        void listView_Refreshing(object sender, System.EventArgs e)
        {
            listView.ItemsSource = GetContainers();

            listView.EndRefresh();
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = GetContainers(e.NewTextValue);
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        private async void Add_Favorites_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Added to Favorites!", "He He", "Ok");
        }

        private async void Remove_Item_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Deleted", "No Dramma", "Ok");

        }


    }
}