using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class Unit
    {
        public Unit()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public Guid UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
