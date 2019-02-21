using RSSController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.Interfaces
{
    public interface IRSSFeedsManageable
    {
        Task<IEnumerable<RSSFeedItem>> GetRSSFeedItems();
    }
}
