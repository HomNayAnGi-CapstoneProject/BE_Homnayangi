using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            Blogs = new HashSet<Blog>();
            OrderCookedDetails = new HashSet<OrderCookedDetail>();
            OrderPackageDetails = new HashSet<OrderPackageDetail>();
            RecipeDetails = new HashSet<RecipeDetail>();
        }

        public Guid RecipeId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? CookedPrice { get; set; }
        public int? MinSize { get; set; }
        public int? MaxSize { get; set; }
        public int? TotalKcal { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<OrderCookedDetail> OrderCookedDetails { get; set; }
        public virtual ICollection<OrderPackageDetail> OrderPackageDetails { get; set; }
        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
