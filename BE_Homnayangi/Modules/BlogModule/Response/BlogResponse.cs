using System;
namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class BlogResponse
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? Reaction { get; set; }
        public int? View { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public decimal? PackagePrice { get; set; }

    }
}
