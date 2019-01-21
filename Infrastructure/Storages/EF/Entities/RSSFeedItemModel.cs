using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Storages.EF.Entities
{
    class RSSFeedItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }
    }
}
