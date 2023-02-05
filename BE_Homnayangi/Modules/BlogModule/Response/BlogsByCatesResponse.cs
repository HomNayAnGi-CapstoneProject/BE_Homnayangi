﻿using System;
namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class BlogsByCatesResponse
    {
        public Guid BlogId { get; set; }
        public string RecipeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Reaction { get; set; }
        public int? View { get; set; }
    }
}

