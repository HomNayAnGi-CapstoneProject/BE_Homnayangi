using System;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Request
{
    public class CreatedAccomplishment
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BlogId { get; set; }
        public string? VideoURL { get; set; }
    }
}
