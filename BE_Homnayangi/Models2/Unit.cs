using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class Unit
    {
        public Guid UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? TypeId { get; set; }
        public bool? Status { get; set; }

        public virtual Type Type { get; set; }
    }
}
