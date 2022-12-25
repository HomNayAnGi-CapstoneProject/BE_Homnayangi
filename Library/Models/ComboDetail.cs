using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class ComboDetail
    {
        public Guid ComboId { get; set; }
        public Guid RecipeId { get; set; }
        public string Description { get; set; }

        public virtual Combo Combo { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
