using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class OrderIngredientDetail
    {
        public Guid OrderId { get; set; }
        public Guid IngredientId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Order Order { get; set; }
    }
}
