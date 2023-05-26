using System;
using System.Collections.Generic;
using Library.Models;
using Microsoft.CodeAnalysis;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogUpdateRequest
    {
        public BlogRequest Blog { get; set; }
        public List<Tuple<PackageUpdateRequest, List<PackageDetailReq>>> Packages { get; set; }
        public List<BlogSubCateRq> BlogSubCates { get; set; }
        public List<BlogReferenceRequest> BlogReferences { get; set; }

        public class PackageDetailReq
        {
            public Guid PackageId { get; set; }
            public Guid IngredientId { get; set; }
            public string Description { get; set; }
            public int? Quantity { get; set; }
        }

        public class BlogSubCateRq {
            public Guid? BlogId { get; set; }
            public Guid? SubCateId { get; set; }
        }

        public class BlogRequest
        {
            public Guid? BlogId { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public int? BlogStatus { get; set; }
            public string VideoUrl { get; set; }
            public int? MinutesToCook { get; set; }
            public bool? IsEvent { get; set; }
            public DateTime? EventExpiredDate { get; set; }
            public Guid? CookingMethodId { get; set; }
            public Guid? RegionId { get; set; }
        }

        public class PackageUpdateRequest
        {
            public Guid PackageId { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public decimal? PackagePrice { get; set; }
            public decimal? CookedPrice { get; set; }
            public int Size { get; set; }
        }
        public BlogUpdateRequest()
        {
            Packages = new List<Tuple<PackageUpdateRequest, List<PackageDetailReq>>>();
        }
        public class BlogReferenceRequest
        {
            public string Text { get; set; }
            public string HTML { get; set; }
            public int Type { get; set; }
        }
    }
}

