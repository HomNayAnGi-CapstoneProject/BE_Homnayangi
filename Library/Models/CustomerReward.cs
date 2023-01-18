using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class CustomerReward
    {
        public Guid CustomerId { get; set; }
        public Guid RewardId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Reward Reward { get; set; }
    }
}
