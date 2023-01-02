using System;
using Library.Models.DTO.IngredientDTO;

namespace Library.Models.DTO.RecipeDetailsDTO
{
	public class RecipeDetailsResponse
	{
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string Description { get; set; }

        public virtual IngredientResponse Ingredient { get; set; }
    }
}

