﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Accomplishments = new HashSet<Accomplishment>();
            BlogReactions = new HashSet<BlogReaction>();
            BlogReferences = new HashSet<BlogReference>();
            BlogSubCates = new HashSet<BlogSubCate>();
            Comments = new HashSet<Comment>();
        }

        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Reaction { get; set; }
        public int? View { get; set; }
        public Guid? AuthorId { get; set; }
        public int? BlogStatus { get; set; }
        public string VideoUrl { get; set; }
        public Guid? RecipeId { get; set; }
        public int? MinutesToCook { get; set; }
        public bool? IsEvent { get; set; }
        public DateTime? EventExpiredDate { get; set; }

        public virtual User Author { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual ICollection<Accomplishment> Accomplishments { get; set; }
        public virtual ICollection<BlogReaction> BlogReactions { get; set; }
        public virtual ICollection<BlogReference> BlogReferences { get; set; }
        public virtual ICollection<BlogSubCate> BlogSubCates { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
