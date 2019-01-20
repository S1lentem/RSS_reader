using RSSController;
using System.Collections.Generic;

namespace RSSReader.Interfaces
{
    interface IRSSFeedsCacheRepository
    {
        void PushInCache(params IEnumerable<RSSFeedItem>[] items);
        IEnumerable<RSSFeedItem> GetFromCache();
    }
}
