using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Region
    {
        public Region()
        {
            Blogs = new HashSet<Blog>();
        }

        public Guid RegionId { get; set; }
        public string RegionName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
