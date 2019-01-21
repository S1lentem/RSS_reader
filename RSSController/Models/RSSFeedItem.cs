﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSController.Models
{
    public class RSSFeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
    }
}