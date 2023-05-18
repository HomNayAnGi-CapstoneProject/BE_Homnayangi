using System;
using BE_Homnayangi.Modules.DTO.IngredientDTO;

namespace BE_Homnayangi.Modules.PackageDetailModule.Response
{
    public class PackageDetailsResponse
    {
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        //public virtual IngredientResponse Ingredient { get; set; }
    }
}

