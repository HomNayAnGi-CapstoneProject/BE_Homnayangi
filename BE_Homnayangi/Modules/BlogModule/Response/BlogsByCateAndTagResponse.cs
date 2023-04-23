using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class BlogsByCateAndTagResponse
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
        public bool IsEvent { get; set; }
        public DateTime? EventExpiredDate { get; set; }
    }
}

