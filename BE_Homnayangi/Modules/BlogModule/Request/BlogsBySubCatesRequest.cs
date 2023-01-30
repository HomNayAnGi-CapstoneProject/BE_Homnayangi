using System;
using System.Collections.Generic;
using Library.PagedList;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogsBySubCatesRequest : PagedRequest
    {
        public string subCateIds { get; set; }

        public BlogsBySubCatesRequest() : base() { }
    }
}

