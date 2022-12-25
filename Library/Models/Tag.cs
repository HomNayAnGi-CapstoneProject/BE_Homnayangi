using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
