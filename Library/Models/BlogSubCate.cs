using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class BlogSubCate
    {
        public Guid BlogId { get; set; }
        public Guid SubCateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual SubCategory SubCate { get; set; }
    }
}
