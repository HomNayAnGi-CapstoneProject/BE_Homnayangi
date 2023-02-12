﻿using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.IngredientModule.IngredientDTO
{
    public class IngredientRequest
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? UnitId { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public int Kcal { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<string> ListImage { get; set; }
        public bool? Status { get; set; }
        public decimal? Price { get; set; }
        public Guid? TypeId { get; set; }
        public string ListImagePosition { get; set; }
    }
}

