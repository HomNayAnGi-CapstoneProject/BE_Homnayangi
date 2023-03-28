using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CustomerBadgeModule.DTO
{
    public class UpdateCustomerBadgeRequest
    {
        public Guid CustomerId { get; set; }
        public Guid BadgeId { get; set; }

        public virtual Badge Badge { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
