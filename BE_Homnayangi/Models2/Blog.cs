using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
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
            Packages = new HashSet<Package>();
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
        public int? MinutesToCook { get; set; }
        public bool? IsEvent { get; set; }
        public int? MinSize { get; set; }
        public int? MaxSize { get; set; }
        public Guid? CookingMethodId { get; set; }
        public Guid? RegionId { get; set; }
        public DateTime? EventExpiredDate { get; set; }

        public virtual User Author { get; set; }
        public virtual CookingMethod CookingMethod { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Accomplishment> Accomplishments { get; set; }
        public virtual ICollection<BlogReaction> BlogReactions { get; set; }
        public virtual ICollection<BlogReference> BlogReferences { get; set; }
        public virtual ICollection<BlogSubCate> BlogSubCates { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
