using Library.Models;
using System.Collections.Generic;
using System;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class BlogDetailResponse
    {
        public Guid BlogId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Preparation { get; set; }

        public string Processing { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? Reaction { get; set; }

        public int? View { get; set; }

        public string AuthorName { get; set; }

        public int? BlogStatus { get; set; }

        public string CategoryName { get; set; }

        public Guid RecipeId { get; set; }

        public string RecipeTitle { get; set; }

        public string RecipeImageURL { get; set; }

        public decimal? RecipePackagePrice { get; set; }

        public decimal? RecipeCookedPrice { get; set; }

        public int? RecipeSize { get; set; }

        // về sau sẽ lấy blogreaction lên để xem user đó react hay chưa => on | off nút react
    }
}
