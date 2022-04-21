using Ventique.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
//using XamFirebase.Models;

namespace Ventique.ViewModels
{
    public class NewsViewModel : BindableObject
    {
        private ObservableCollection<GroupedClientNewsModel> newsItem;
        public ObservableCollection<GroupedClientNewsModel> NewsItem
        {
            get { return newsItem; }
            set
            {
                newsItem = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshList { get; set; }
        public NewsViewModel()
        {
            NewsItem = new ObservableCollection<GroupedClientNewsModel>();
            RefreshList = new Command(async () => await PerformRefresh());
            GetNewsInformation();
        }
        private async Task PerformRefresh()
        {
            NewsItem = new ObservableCollection<GroupedClientNewsModel>();
            GetNewsInformation();
        }
        private async void GetNewsInformation()
        {
            FirebaseClient fc = new FirebaseClient("https://online-shop-26853-default-rtdb.firebaseio.com/");
            var GetNews = (await fc
                .Child("Containers")
                .OnceAsync<Containers>()).Select(item => new Containers
                {
                    ImageUrl = item.Object.ImageUrl,
                    Name = item.Object.Name,
                    Status = item.Object.Status
                }).ToList().OrderBy(i => i.Name) ;
            var headergroup = GetNews.Select(x => x.Name).Distinct().ToList();

            foreach (var item in headergroup)
            {
                var newsGroup = new GroupedClientNewsModel() { Name = item };
                var contents = GetNews.Where(i=> i.Name == item).ToList();
                if (contents.Count != 0)
                {
                    foreach(var groupitems in contents)
                    {
                        newsGroup.Add(new ClientNewsModel() { IteamName = groupitems.Name });
                        newsGroup.Add(new ClientNewsModel() { IteamImage = groupitems.ImageUrl });
                        newsGroup.Add(new ClientNewsModel() { IteamStatus = groupitems.Status });
                        
                    }
                    NewsItem.Add(newsGroup);
                }
            }

        }
    }
}
