using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.RecipeModule.RecipeDTO;
using BE_Homnayangi.Modules.SubCateModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class BlogDetailResponse : BlogResponse
    {
        // Blog information
        public string Preparation { get; set; }
        public string Processing { get; set; }
        public string Finished { get; set; }
        public string VideoUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? BlogStatus { get; set; }


        // Recipe information
        public Guid RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string RecipeImageURL { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? CookedPrice { get; set; }
        public int? MaxSize { get; set; }
        public int? MinSize { get; set; }


        // List SubCates
        public List<SubCateResponse> SubCates { get; set; }


        // List Ingredients
        public List<RecipeDetail> RecipeDetails { get; set; }



        // Note: về sau sẽ lấy blogreaction lên để xem user đó react hay chưa => on | off nút react
    }
}
