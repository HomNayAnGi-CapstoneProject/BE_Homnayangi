using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Request
{
    public class CreatedAccomplishment
    {
        public string Content { get; set; }
        public Guid BlogId { get; set; }
        public ICollection<string> ListImage { get; set; }
        public ICollection<string> ListVideo { get; set; }
    }
}
