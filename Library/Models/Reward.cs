using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Reward
    {
        public Reward()
        {
            CustomerRewards = new HashSet<CustomerReward>();
        }
        public Guid RewardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ImageUrl { get; set; }
        public bool? Status { get; set; }
        public int? ConditionType { get; set; }
        public int? ConditionValue { get; set; }

        public virtual ICollection<CustomerReward> CustomerRewards { get; set; }
    }
}
