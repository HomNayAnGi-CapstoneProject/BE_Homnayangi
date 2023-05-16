using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class CookingMethod
    {
        public CookingMethod()
        {
            Blogs = new HashSet<Blog>();
        }

        public Guid CookingMethodId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
