using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            BlogSubCates = new HashSet<BlogSubCate>();
        }

        public Guid SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BlogSubCate> BlogSubCates { get; set; }
    }
}
