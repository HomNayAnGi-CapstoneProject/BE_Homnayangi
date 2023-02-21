using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class BlogReference
    {
        public Guid BlogReferenceId { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public int Type { get; set; }
        public Guid? BlogId { get; set; }
        public int? Status { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
