using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class PackageDetail
    {
        public Guid PackageId { get; set; }
        public Guid IngredientId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int? Status { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Package Package { get; set; }
    }
}
