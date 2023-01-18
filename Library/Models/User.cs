using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class User
    {
        public User()
        {
            Accomplishments = new HashSet<Accomplishment>();
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            Vouchers = new HashSet<Voucher>();
        }

        public Guid UserId { get; set; }
        public string Displayname { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public int? Gender { get; set; }
        public string Avatar { get; set; }
        public int? Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsGoogle { get; set; }

        public virtual ICollection<Accomplishment> Accomplishments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
