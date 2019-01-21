using Infrastructure.Storages.EF.Entities;
using Microsoft.EntityFrameworkCore;
using RSSController;
using RSSController.Interfaces;
using System;
using System.Collections.Generic;

namespace Infrastructure.Storages.EF
{
    public class SQLiteRSSFeedsCacheRepository : IRSSFeedsCacheRepository
    {
        public SQLiteRSSFeedsCacheRepository()
        {
            using (var context = new RSSFeedsContext())
            {
                context.Database.Migrate();
            }
        }

        public void ClearCache()
        {
            using (var context = new RSSFeedsContext())
            {
                context.RSSFeedItemModels.RemoveRange(context.RSSFeedItemModels);
                context.SaveChanges();
            }
        }

        public IEnumerable<RSSFeedItem> GetFromCache()
        {
            using (var context = new RSSFeedsContext())
            {
                var allFeeds = new List<RSSFeedItem>();
                foreach (var item in context.RSSFeedItemModels)
                {
                    allFeeds.Add(new RSSFeedItem()
                    {
                        Title = item.Title,
                        Description = item.Desciption,
                        Date = item.Date,
                        Link = item.Link,
                        ImageUrl = item.ImageLink
                    });
                }
                return allFeeds;
            }
        }

        public void PushInCache(params RSSFeedItem[] items)
        {
            using (var context = new RSSFeedsContext())
            {
                foreach (var item in items)
                {
                    context.RSSFeedItemModels.Add(new RSSFeedItemModel()
                    {
                        Title = item.Title,
                        Desciption = item.Description,
                        Date = item.Date,
                        Link = item.Link,
                        ImageLink = item.ImageUrl
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
