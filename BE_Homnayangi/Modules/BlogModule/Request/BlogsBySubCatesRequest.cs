using System;
using System.Collections.Generic;
using Library.PagedList;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogsBySubCatesRequest : PagedRequest
    {
        public string SubCateIds { get; set; }
        public string? SearchString { get; set; }

        public BlogsBySubCatesRequest() : base() { }
    }
}

