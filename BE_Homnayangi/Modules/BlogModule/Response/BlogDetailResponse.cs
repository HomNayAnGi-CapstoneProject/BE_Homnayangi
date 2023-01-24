using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.RecipeModule.RecipeDTO;
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
        public decimal? RecipeCookedPrice { get; set; }
        public int? RecipeSize { get; set; }

        // List RecipeDetails
        public List<RecipeDetailsResponse> RecipeDetailss { get; set; }


        // List SubCates
        public IDictionary<Guid, List<string>> SubCates { get; set; }


        // List Ingredients
        public List<IngredientResponse> Ingredients { get; set; }



        // Note: về sau sẽ lấy blogreaction lên để xem user đó react hay chưa => on | off nút react
    }
}
