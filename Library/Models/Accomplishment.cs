﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Accomplishment
    {
        public Guid AccomplishmentId { get; set; }
        public string Content { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }
        public Guid? BlogId { get; set; }

        public virtual Customer Author { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
