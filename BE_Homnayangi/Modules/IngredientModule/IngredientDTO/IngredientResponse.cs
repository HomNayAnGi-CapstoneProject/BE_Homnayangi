using Library.Models;
using System;
using System.Collections.Generic;
using Type = Library.Models.Type;

namespace BE_Homnayangi.Modules.DTO.IngredientDTO
{
    public class IngredientResponse
    {
        public Guid IngredientId { get; set; }
        public Guid? UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public string UnitName { get; set; }
        public string Picture { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<string> ListImage { get; set; }
        public bool? Status { get; set; }
        public decimal? Price { get; set; }
        public Guid? TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public string ListImagePosition { get; set; }
    }
}

