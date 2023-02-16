using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class CartDetail
    {
        public Guid CartId { get; set; }
        public Guid ItemId { get; set; }
        public bool IsCooked { get; set; }
        /*
            false: Ingredient | package_ingredient
            true: cooked
        */
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        
        public virtual Cart Cart { get; set; }
    }
}
