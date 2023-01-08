using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Orders = new HashSet<Order>();
            Vouchers = new HashSet<Voucher>();
            Accomplishments = new HashSet<Accomplishment>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }
        public int? Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsGoogle { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<Accomplishment> Accomplishments { get; set; }
    }
}
