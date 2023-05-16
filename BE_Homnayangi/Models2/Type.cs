using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class Type
    {
        public Type()
        {
            Ingredients = new HashSet<Ingredient>();
            Units = new HashSet<Unit>();
        }

        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}
