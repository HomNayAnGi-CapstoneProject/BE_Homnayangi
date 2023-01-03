﻿using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class GetBlogsForHomePageResponse
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public List<string> ListTagName { get; set; }
        public decimal? PackagePrice { get; set; }
    }
}