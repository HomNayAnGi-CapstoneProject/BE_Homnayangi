﻿using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class OverviewBlog
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int? View { get; set; }
        public int? Reaction { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalKcal { get; set; }

        public string? AuthorName { get; set; }
        public int? Status { get; set; }

        public bool? IsEvent { get; set; }
        public DateTime? EventExpiredDate { get; set; }
    }
}
