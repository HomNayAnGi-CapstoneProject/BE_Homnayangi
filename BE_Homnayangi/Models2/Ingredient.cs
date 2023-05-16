using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PackageDetails = new HashSet<PackageDetail>();
        }

        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public string Picture { get; set; }
        public int? Kcal { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Status { get; set; }
        public decimal? Price { get; set; }
        public string ListImage { get; set; }
        public Guid? TypeId { get; set; }
        public string ListImagePosition { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<PackageDetail> PackageDetails { get; set; }
    }
}
