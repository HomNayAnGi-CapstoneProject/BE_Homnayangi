using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.IngredientModule.Request
{
    public class UpdatedIngredientRequest
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantitative { get; set; }
        public ICollection<string> ListImage { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public Guid TypeId { get; set; }
        public bool? Status { get; set; }
        public string ListImagePosition { get; set; }
    }
}
