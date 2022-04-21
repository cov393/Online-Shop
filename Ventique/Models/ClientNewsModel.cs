using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ventique.Models
{
    public class ClientNewsModel
    {
        public string IteamName { get; set; }
        public string IteamImage { get; set; } 
        public string IteamStatus { get; set; }
        
    }
    public class GroupedClientNewsModel : ObservableCollection<ClientNewsModel>
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
