using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class SearchBlogsResponse
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
    }
}
