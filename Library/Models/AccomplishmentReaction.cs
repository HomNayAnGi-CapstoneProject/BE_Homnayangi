using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class AccomplishmentReaction
    {
        public Guid AccomplishmentId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual Accomplishment Accomplishment { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
