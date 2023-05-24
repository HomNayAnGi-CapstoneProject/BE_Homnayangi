using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Type
    {
        public Type()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
