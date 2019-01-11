using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Web.Syndication;

namespace RSSController
{
    public class RSSFeedController
    {
        private readonly string itemSection = "item";
        private readonly string titleTag = "title";
        private readonly string descriptionTag = "description";
        private readonly string dateTag = "pubDate";
        private readonly string linkTag = "link";

        private string url;

        public string URL { get => url;
            set
            {
                url = value;
                AllFeeds = null;
            }
        }
        public List<RSSFeed> AllFeeds { get; private set; } = null;


        public RSSFeedController(string url) => this.url = url;

        private async Task<IEnumerable<RSSFeed>> LoadFeeds()
        {
            var feeds = new List<RSSFeed>();

            var client = new HttpClient();
            XDocument document = XDocument.Load(await client.GetStreamAsync(URL));

            foreach (var item in document.Descendants(itemSection))
            {
                feeds.Add(new RSSFeed()
                {
                    Title = item.Element(titleTag)?.Value,
                    Description = item.Element(descriptionTag)?.Value,
                    Date = DateTime.Parse(item.Element(dateTag)?.Value),
                    Link = item.Element(linkTag)?.Value
                });
            }
            this.AllFeeds = new List<RSSFeed>(feeds);
            return feeds;
        }

        public async Task<IEnumerable<RSSFeed>> GetFeeds()
        {
            if (this.AllFeeds == null)
            {
                return await LoadFeeds();
            }
            return this.AllFeeds;
        }
    }
}
