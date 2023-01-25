using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class GetBlogsForHomePageResponse
    {
        public Guid BlogId { get; set; }
        public string RecipeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ListSubCateName { get; set; }
        public decimal? PackagePrice { get; set; }
        public int? Reaction { get; set; }
        public int? View { get; set; }
        public Guid? SubCateId { get; set; } 
    }
}
