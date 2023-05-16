using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class BadgeCondition
    {
        public Guid BadgeConditionId { get; set; }
        public int? Accomplishments { get; set; }
        public int? Orders { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }
        public Guid BadgeId { get; set; }

        public virtual Badge Badge { get; set; }
    }
}
