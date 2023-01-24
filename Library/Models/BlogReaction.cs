using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class BlogReaction
    {
        public Guid BlogId { get; set; }
        public Guid CustomerId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
