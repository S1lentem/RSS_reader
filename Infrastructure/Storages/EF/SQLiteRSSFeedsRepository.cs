using Infrastructure.Storages.EF.Entities;
using Microsoft.EntityFrameworkCore;
using RSSController.Interfaces;
using RSSController.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Storages.EF
{
    public class SQLiteRSSFeedsRepository : IRSSFeedsRepository
    {
        public SQLiteRSSFeedsRepository()
        {
            Task.Run(() =>
            {
                using (var context = new RSSFeedsContext()) { }
            });
        }

        public void AddRSSFeed(string title, string url, bool isCurrent=false)
        {
            using (var context = new RSSFeedsContext())
            {
                var model = context.RSSFeedModels
                    .FirstOrDefault(item => item.Title == title && item.URL == url);

                // Search currrent RSS-feed
                var current = context.RSSFeedModels.FirstOrDefault(item => item.IsCurrent);
                if (current != null)
                {
                    current.IsCurrent = false;
                }

                if (model == null)
                {
                    
                    context.RSSFeedModels.Add(new RSSFeedModel()
                    {
                        Title = title,
                        URL = url,
                        IsCurrent = isCurrent
                    });
                }
                else
                {
                    model.IsCurrent = isCurrent;
                    context.RSSFeedModels.Update(model);
                }
                context.SaveChanges();
            }
        }

        public IEnumerable<RSSFeed> GetAllRSSFeeds()
        {
            using (var context = new RSSFeedsContext())
            {
                return context.RSSFeedModels.ToList()
                    .Select(model => new RSSFeed(model.Title, model.URL));
            }
            
        }


        public RSSFeed GetCurrentRSSFeed()
        {
            RSSFeedModel model = null;
            using (var context = new RSSFeedsContext())
            {

                model = context.RSSFeedModels
                    .FirstOrDefault(item => item.IsCurrent);
            }
            return model != null ? new RSSFeed(model.Title, model.URL) : null;
        }

        public void SetCurrentByURL(string url)
        {
            using (var context = new RSSFeedsContext())
            {
                var model = context.RSSFeedModels
                    .FirstOrDefault(item => item.URL == url) ?? throw new ArgumentException($"{url} not found");
                model.IsCurrent = true;
                context.RSSFeedModels.Update(model);
                context.SaveChanges();
            }
        }
    }
}
