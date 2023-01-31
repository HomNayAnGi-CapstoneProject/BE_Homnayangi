using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            OrderIngredientDetails = new HashSet<OrderIngredientDetail>();
            RecipeDetails = new HashSet<RecipeDetail>();
        }

        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantitative { get; set; }
        public string Picture { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Status { get; set; }
        public decimal? Price { get; set; }
        public string ListImage { get; set; }
        public Guid? TypeId { get; set; }
        public string ListImagePosition { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<OrderIngredientDetail> OrderIngredientDetails { get; set; }
        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
