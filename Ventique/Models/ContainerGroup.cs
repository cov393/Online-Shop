using System;
using System.Collections.Generic;
using System.Text;

namespace Ventique.Models
{
    public class ContainerGroup : List<Containers>
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public ContainerGroup(string title, string shorttitle)
        {
            Title = title;
            ShortTitle = shorttitle;
        }
    }
}
