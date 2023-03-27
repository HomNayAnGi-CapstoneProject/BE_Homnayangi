﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid IngredientId { get; set; }
        public int? IngredientQuantity { get; set; }
        public Guid RecipeId { get; set; }
        public int? RecipeQuantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Order Order { get; set; }
    }
}
