using System;
using BE_Homnayangi.Modules.DTO.IngredientDTO;

namespace BE_Homnayangi.Modules.DTO.RecipeDetailsDTO
{
	public class RecipeDetailsResponse
	{
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string Description { get; set; }

        //public virtual IngredientResponse Ingredient { get; set; }
    }
}

