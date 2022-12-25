using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Notification
    {
        public Guid NotificationId { get; set; }
        public string Description { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual Order Order { get; set; }
        public virtual Customer User { get; set; }
    }
}
