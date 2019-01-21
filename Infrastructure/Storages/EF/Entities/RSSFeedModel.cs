using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Storages.EF.Entities
{
    class RSSFeedModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string URL { get; set; }
        public bool IsCurrent { get; set; }
    }
}
