﻿using Infrastructure.Storages.EF.Entities;
using RSSController.Interfaces;
using RSSController.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Storages.EF
{
    public class SQLiteRSSFeedsRepository : IRSSFeedsRepository
    {
        public void AddRSSFeed(string title, string url, bool isCurrent=false)
        {
            using (var context = new RSSFeedsContext())
            {
                context.RSSFeedModels.Add(new RSSFeedModel()
                {
                    Title = title,
                    URL = url,
                    IsCurrent = isCurrent
                });
                context.SaveChanges();
            }
        }

        public IEnumerable<RSSFeed> GetAllRSSFeeds()
        {
            using (var context = new RSSFeedsContext())
            {
                return context.RSSFeedModels
                    .Select(model => new RSSFeed(model.Title, model.URL));
            }
        }

        public RSSFeed GetCurrentRSSFeed()
        {
            using (var context = new RSSFeedsContext())
            {
                var model = context.RSSFeedModels
                    .FirstOrDefault(item => item.IsCurrent);
                return model != null ? new RSSFeed(model.Title, model.URL) : null;
            }
        }
    }
}
