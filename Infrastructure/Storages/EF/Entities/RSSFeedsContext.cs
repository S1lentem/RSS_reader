using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Storages.EF.Entities
{
    class RSSFeedsContext : DbContext
    {
        private readonly string _connectionString = "Filename=Mobile.db";

       
        public DbSet<RSSFeedItemModel> RSSFeedItemModels { get; set; }

        public RSSFeedsContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(_connectionString);
    }
}
