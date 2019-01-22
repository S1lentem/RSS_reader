using RSSController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSController.Interfaces
{
    public interface IRSSFeedsRepository
    {
        void AddRSSFeed(string title, string url, bool isCurrent = false);
        RSSFeed GetCurrentRSSFeed();
        IEnumerable<RSSFeed> GetAllRSSFeeds();
        void SetCurrentByURL(string url);
    }
}
