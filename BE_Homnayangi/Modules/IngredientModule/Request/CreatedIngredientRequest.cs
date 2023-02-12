using Library.Models;
using System.Collections.Generic;
using System;

namespace BE_Homnayangi.Modules.IngredientModule.Request
{
    public class CreatedIngredientRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? UnitId { get; set; }
        public string Picture { get; set; }
        public int Kcal { get; set; }
        public ICollection<string> ListImage { get; set; }
        public decimal Price { get; set; }
        public Guid TypeId { get; set; }
        public string ListImagePosition { get; set; }
    }
}
