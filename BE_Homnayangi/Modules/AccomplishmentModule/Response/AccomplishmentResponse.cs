using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Response
{
    public class AccomplishmentResponse
    {
        public Guid AuthorId { get; set; }
        public Guid AccomplishmentId { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }
        public ICollection<string>? ListImage { get; set; }
        public ICollection<string>? ListVideo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorFullName { get; set; }
        public string Avatar { get; set; }
        public int Reaction { get; set; }
    }
}
