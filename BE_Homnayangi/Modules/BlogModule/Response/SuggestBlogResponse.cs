using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class SuggestBlogResponse
    {
        public List<OverviewBlogResponse> SuggestBlogs { get; set; }
        public int? Calo { get; set; }
    }
}
