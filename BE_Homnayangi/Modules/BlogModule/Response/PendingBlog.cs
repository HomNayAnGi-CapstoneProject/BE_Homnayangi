using System;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class PendingBlog
    {
        public Guid BlogId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorName { get; set; }
    }
}
