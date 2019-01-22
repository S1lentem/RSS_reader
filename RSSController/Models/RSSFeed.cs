using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSController.Models
{
    public class RSSFeed
    {
        public string Title { get; }
        public string Link { get; }

        public RSSFeed(string title, string link)
        {
            Title = title;
            Link = link;
        }
    }
}
