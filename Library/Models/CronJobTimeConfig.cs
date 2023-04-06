using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class CronJobTimeConfig
    {
        public Guid CronJobTimeConfigId { get; set; }
        public int? Minute { get; set; }
        public int? Hour { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? TargetObject { get; set; }
    }
}
