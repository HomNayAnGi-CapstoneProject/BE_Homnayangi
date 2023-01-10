using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accomplishments = new HashSet<Accomplishment>();
            BlogReactions = new HashSet<BlogReaction>();
            Comments = new HashSet<Comment>();
            CustomerVouchers = new HashSet<CustomerVoucher>();
            Notifications = new HashSet<Notification>();
            Transactions = new HashSet<Transaction>();
            UserRewards = new HashSet<UserReward>();
        }

        public Guid CustomerId { get; set; }
        public string? Username { get; set; }
        public string? Displayname { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Phonenumber { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsGoogle { get; set; }

        public virtual ICollection<Accomplishment> Accomplishments { get; set; }
        public virtual ICollection<BlogReaction> BlogReactions { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CustomerVoucher> CustomerVouchers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserReward> UserRewards { get; set; }
    }
}
