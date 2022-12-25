﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class OrderCookedDetail
    {
        public Guid OrderId { get; set; }
        public Guid RecipeId { get; set; }
        public int? Quantity { get; set; }
        public int? Session { get; set; }
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
