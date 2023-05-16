using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class CaloReference
    {
        public Guid CaloReferenceId { get; set; }
        public int? FromAge { get; set; }
        public int? ToAge { get; set; }
        public int? Calo { get; set; }
        public bool? IsMale { get; set; }
    }
}
