using System;
using System.Collections;
using System.Collections.Generic;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using Library.Models;

namespace BE_Homnayangi.Modules.DTO.RecipeDTO
{
	public class RecipeResponse
	{
        public Guid RecipeId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? CookedPrice { get; set; }
        public int? Region { get; set; }
        public int? Size { get; set; }

        public virtual Blog RecipeNavigation { get; set; }
        public virtual ICollection<RecipeDetailsResponse> RecipeDetails { get; set; }

    }
}

