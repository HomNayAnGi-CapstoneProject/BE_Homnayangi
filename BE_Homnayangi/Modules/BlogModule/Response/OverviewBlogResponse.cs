using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class OverviewBlogResponse
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ListSubCateName { get; set; }
        public string ImageUrl { get; set; }
        public int TotalKcal { get; set; }
        public decimal PackagePrice { get; set; }
        public bool IsEvent { get; set; }
        public DateTime? EventExpiredDate { get; set; }

    }
}
