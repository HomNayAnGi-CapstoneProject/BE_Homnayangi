using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsCombo { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
