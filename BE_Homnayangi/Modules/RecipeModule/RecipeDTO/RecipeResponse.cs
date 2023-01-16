using System;
using System.Collections;
using System.Collections.Generic;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.RecipeModule.RecipeDTO;
using Library.Models;

namespace BE_Homnayangi.Modules.DTO.RecipeDTO
{
	public class RecipeResponse : RecipeBase
	{
        public virtual Blog RecipeNavigation { get; set; }
        public virtual ICollection<RecipeDetailsResponse> RecipeDetails { get; set; }

    }
}

