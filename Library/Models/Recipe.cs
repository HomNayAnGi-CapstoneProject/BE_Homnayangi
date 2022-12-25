using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            ComboDetails = new HashSet<ComboDetail>();
            OrderCookedDetails = new HashSet<OrderCookedDetail>();
            OrderPackageDetails = new HashSet<OrderPackageDetail>();
            RecipeDetails = new HashSet<RecipeDetail>();
        }

        public Guid RecipeId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? CookedPrice { get; set; }
        public int? Region { get; set; }
        public int? Size { get; set; }

        public virtual Blog RecipeNavigation { get; set; }
        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
        public virtual ICollection<OrderCookedDetail> OrderCookedDetails { get; set; }
        public virtual ICollection<OrderPackageDetail> OrderPackageDetails { get; set; }
        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
