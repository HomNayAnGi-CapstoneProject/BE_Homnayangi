using System;
using System.Collections.Generic;
using Library.Models;
using Microsoft.CodeAnalysis;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogUpdateRequest
    {
        public Blog Blog { get; set; }
        public IDictionary<PackageUpdateRequest, List<PackageDetail>> Packages { get; set; }
        public List<BlogSubCate> BlogSubCates { get; set; }
        public List<BlogReferenceRequest> BlogReferences { get; set; }

        public class PackageUpdateRequest
        {
            public Guid PackageId { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public decimal? PackagePrice { get; set; }
            public decimal? CookedPrice { get; set; }
            public int Size { get; set; }
            public Guid? BlogId { get; set; }
        }
        public BlogUpdateRequest()
        {
            Packages = new Dictionary<PackageUpdateRequest, List<PackageDetail>>();
            BlogSubCates = new List<BlogSubCate>();
        }
        public class BlogReferenceRequest
        {
            public string Text { get; set; }
            public string HTML { get; set; }
            public int Type { get; set; }
        }
    }
}

