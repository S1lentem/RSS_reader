using RSSController;
using RSSController.Interfaces;
using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storages
{
    class SQLiteRSSFeedsCacheRepository : DbContext, IRSSFeedsCacheRepository
    {
       
        public IEnumerable<RSSFeedItem> GetFromCache()
        {
            throw new NotImplementedException();
        }

        public void PushInCache(params IEnumerable<RSSFeedItem>[] items)
        {
            throw new NotImplementedException();
        }
    }
}
