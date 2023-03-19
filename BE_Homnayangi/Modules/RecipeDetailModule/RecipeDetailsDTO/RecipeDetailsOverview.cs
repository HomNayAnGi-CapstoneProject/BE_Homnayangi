using System;

namespace BE_Homnayangi.Modules.RecipeDetailModule.RecipeDetailsDTO
{
    public class RecipeDetailsOverview
    {
        public Guid RecipeId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
    }
}
