using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Category
    {
        public Category()
        {
            Blogs = new HashSet<Blog>();
            Tags = new HashSet<Tag>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
