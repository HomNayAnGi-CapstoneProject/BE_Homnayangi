using BE_Homnayangi.Modules.SubCateModule.Response;
using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class BlogDetailResponse
    {
        public BlogDetailResponse()
        {
            Packages = new Dictionary<PackagesResponse, List<PackageDetailResponse>>();
        }
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string DescriptionText { get; set; }
        public string DescriptionHTML { get; set; }
        public string PreparationText { get; set; }
        public string PreparationHTML { get; set; }
        public string ProcessingText { get; set; }
        public string ProcessingHTML { get; set; }
        public string FinishedText { get; set; }
        public string FinishedHTML { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int? Reaction { get; set; }
        public int? View { get; set; }
        public string AuthorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? BlogStatus { get; set; }
        public int? MinutesToCook { get; set; }
        public bool? IsEvent { get; set; }
        public DateTime? EventExpiredDate { get; set; }
        public int? TotalKcal { get; set; }
        public int? MaxSize { get; set; }
        public int? MinSize { get; set; }

        //List Packages
        public IDictionary<PackagesResponse, List<PackageDetailResponse>> Packages { get; set; }


        // List SubCates
        public List<SubCateResponse> SubCates { get; set; }

        // Related blogs
        public List<BlogsByCatesResponse> RelatedBlogs { get; set; }

        // Note: về sau sẽ lấy blogreaction lên để xem user đó react hay chưa => on | off nút react
    }

    public class PackagesResponse
    {
        public bool? IsCooked { get; set; }
        public decimal? PackagePrice { get; set; }

        // Package information
        public Guid PackageId { get; set; }
        public string PackageTitle { get; set; }
        public string PackageImageURL { get; set; }
        public int Size { get; set; }
    }

    public class PackageDetailResponse
    {
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int? Kcal { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public string UnitName { get; set; }
    }
}
