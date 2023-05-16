using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Content { get; set; }
        public bool? Status { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? BlogId { get; set; }
        public bool? ByStaff { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
