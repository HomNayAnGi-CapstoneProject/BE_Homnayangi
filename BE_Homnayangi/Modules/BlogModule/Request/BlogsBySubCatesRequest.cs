using Library.PagedList;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogsBySubCatesRequest : PagedRequest
    {
        public string SubCateIds { get; set; }
        public string? SearchString { get; set; }
        public bool? IsEvent{ get; set; }

        public BlogsBySubCatesRequest() : base() { }
    }
}

