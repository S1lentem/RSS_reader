using RSSController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Interfaces
{
    public interface IRSSFeedsManageable
    {
        Task<IEnumerable<RSSFeedItem>> GetRssFeedAsync();
        void SetNewsSource(string url);
    }
}
