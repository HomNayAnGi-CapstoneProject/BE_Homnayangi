using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class UserReward
    {
        public Guid UserId { get; set; }
        public Guid RewardId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Reward Reward { get; set; }
        public virtual Customer User { get; set; }
    }
}
