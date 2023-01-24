using System;
using Library.PagedList;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogFilterByCateAndTagRequest : PagedRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? TagId { get; set; }

        public BlogFilterByCateAndTagRequest() : base() {}
    }
}

