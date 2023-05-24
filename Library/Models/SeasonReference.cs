using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class SeasonReference
    {
        public Guid SeasonReferenceId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
    }
}
