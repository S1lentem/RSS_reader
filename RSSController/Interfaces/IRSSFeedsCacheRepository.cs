using RSSController;
using System.Collections.Generic;

namespace RSSController.Interfaces
{
    public interface IRSSFeedsCacheRepository
    {
        void PushInCache(params RSSFeedItem[] items);
        IEnumerable<RSSFeedItem> GetFromCache();
        void ClearCache();
    }
}
