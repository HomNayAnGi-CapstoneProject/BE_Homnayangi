using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Combo
    {
        public Combo()
        {
            ComboDetails = new HashSet<ComboDetail>();
        }

        public Guid ComboId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? CookedPrice { get; set; }
        public int? Size { get; set; }

        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
    }
}
