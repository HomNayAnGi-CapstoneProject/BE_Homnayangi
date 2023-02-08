using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class RecipeDetail
    {
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
