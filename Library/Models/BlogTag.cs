using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class BlogTag
    {
        public Guid BlogId { get; set; }
        public Guid TagId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
