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
        private readonly string imageLinkTag = "enclosure";

        private readonly string urlAttribute = "url";

        private string url;
        private IEnumerable<RSSFeedItem> news;


        public string URL { get => url;
            set
            {
                url = value;
                news = null;
            }
        }
        
        public RSSFeedController(string url) => this.url = url;

        public RSSFeedController() : this(null) { }

        private async Task<IEnumerable<RSSFeedItem>> LoadFeeds()
        {
            var feeds = new List<RSSFeedItem>();

            var client = new HttpClient();
            XDocument document = XDocument.Load(await client.GetStreamAsync(URL));

            foreach (var item in document.Descendants(itemSection))
            {
                feeds.Add(new RSSFeedItem()
                {
                    Title = item.Element(titleTag)?.Value,
                    Description = item.Element(descriptionTag)?.Value?.ClearFromHtml(),
                    Date = DateTime.Parse(item.Element(dateTag)?.Value),
                    Link = item.Element(linkTag)?.Value,
                    ImageUrl = item.Element(imageLinkTag)?.Attribute(urlAttribute)?.Value
                });
            }
            this.news = new List<RSSFeedItem>(feeds);
            return feeds;
        }

        public async Task<IEnumerable<RSSFeedItem>> GetFeeds()
        {
            if (this.news == null && this.URL != null)
            {
                return await LoadFeeds();
            }
            return this.news;
        }
    }
}
