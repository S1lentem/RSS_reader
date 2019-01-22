using RSSController.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSReader.Interfaces
{
    interface IRSSFeedsManageable
    {
        Task<IEnumerable<RSSFeedItem>> GetRssFeedAsync();
        void SetNewsSource(string link);
        string GetCurrentNewsSource();
    }
}
