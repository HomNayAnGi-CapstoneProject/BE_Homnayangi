﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class PriceNote
    {
        public Guid PriceNoteId { get; set; }
        public Guid? IngredientId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateApply { get; set; }
        public bool? Status { get; set; }
    }
}
